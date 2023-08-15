using System.Net;
using Microsoft.Extensions.Logging;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public sealed class InternalInvalidOperationDescriptor : IExceptionDescriptor
    {
        private readonly ILogger<InternalInvalidOperationDescriptor> _logger;

        public InternalInvalidOperationDescriptor(ILogger<InternalInvalidOperationDescriptor> logger)
        {
            _logger = logger;
        }

        public bool CanHandle(Exception ex)
        {
            return ex is InternalInvalidOperationException;
        }

        public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

        public ErrorResult Handle(Exception ex)
        {
            _logger.LogError(ex, "BusinessLogicException");

            string message = string.IsNullOrWhiteSpace(ex.Message) ? "Ошибка. Недопустимая операция." : ex.Message;

            var errors = new[]
            {
                new ErrorProperty(nameof(InternalInvalidOperationException), message)
            };

            return new ErrorResult(errors);
        }
    }
}
