using System.Net;
using UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors;
using UserService.Infrastructure.SeedWork.JsonSerializers;

namespace UserService.Api.Middleware
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IEnumerable<IExceptionDescriptor> _exceptionDescriptors;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger,
            IJsonSerializer jsonSerializer,
            IHostEnvironment hostEnvironment,
            IEnumerable<IExceptionDescriptor> exceptionDescriptors)
        {
            _next = next;
            _logger = logger;
            _jsonSerializer = jsonSerializer;
            _hostEnvironment = hostEnvironment;
            _exceptionDescriptors = exceptionDescriptors;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                if (!Handle(context, exception))
                {
                    _logger.LogError(exception, "Unhandled exception.");

                    var errors = _hostEnvironment.IsDevelopment()
                    ? ConvertToDevelop(exception)
                    : new[]
                    {
                        new ErrorProperty(nameof(Exception.Message), HttpStatusCode.InternalServerError.ToString())
                    };

                    WriteResponse(context, HttpStatusCode.InternalServerError, new ErrorResult(errors));
                }
            }
        }

        private bool Handle(HttpContext context, Exception exception)
        {
            try
            {
                foreach (var exceptionDescriptor in _exceptionDescriptors)
                {
                    if (exceptionDescriptor.CanHandle(exception))
                    {
                        var exceptionResponse = exceptionDescriptor.Handle(exception);

                        WriteResponse(context, exceptionDescriptor.StatusCode, exceptionResponse);

                        return true;
                    }
                }
            }
            catch (Exception innerException)
            {
                _logger.LogError(innerException, "Exception matching was failed.");
            }

            return false;
        }

        private ErrorProperty[]? ConvertToDevelop(Exception exception)
        {
            var errors = exception.InnerException == null
                ? new[] {
                    new ErrorProperty(nameof(Exception.Message), exception.Message),
                    new ErrorProperty(nameof(Exception.StackTrace), exception.StackTrace)
                }
                : new[] {
                    new ErrorProperty(nameof(Exception.Message), exception.Message),
                    new ErrorProperty(nameof(Exception.StackTrace), exception.StackTrace),
                    new ErrorProperty($"{nameof(Exception.InnerException)}_{nameof(Exception.Message)}", exception.InnerException.Message),
                    new ErrorProperty($"{nameof(Exception.InnerException)}_{nameof(Exception.StackTrace)}", exception.InnerException.StackTrace)
                };

            return errors;
        }

        private void WriteResponse(HttpContext context, HttpStatusCode statusCode, ErrorResult errorResult)
        {
            var data = _jsonSerializer.Serialize(errorResult);

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            context.Response.WriteAsync(data);
        }
    }
}