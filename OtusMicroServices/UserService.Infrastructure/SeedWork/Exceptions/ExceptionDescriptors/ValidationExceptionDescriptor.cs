using System.Net;
using FluentValidation;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public sealed class ValidationExceptionDescriptor : IExceptionDescriptor
    {
        public bool CanHandle(Exception ex)
        {
            return ex is ValidationException;
        }

        public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

        public ErrorResult Handle(Exception ex)
        {
            var validationException = (ValidationException)ex;

            var errors = validationException.Errors
                .Select(e => new ErrorProperty(e.PropertyName, e.ErrorMessage))
                .ToArray();

            return new ErrorResult(errors);
        }
    }
}
