using System.Net;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public class AccessValidationExceptionDescriptor : IExceptionDescriptor
    {
        public bool CanHandle(Exception ex)
        {
            return ex is UnauthorizedAccessException;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public ErrorResult Handle(Exception ex)
        {
            var validationException = (UnauthorizedAccessException)ex;

            var error = new ErrorProperty(nameof(UnauthorizedAccessException), validationException.Message);

            return new ErrorResult(error);
        }
    }
}