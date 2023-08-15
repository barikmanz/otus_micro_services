using System.Net;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public class AccessForbiddenExceptionDescriptor : IExceptionDescriptor
    {
        public bool CanHandle(Exception ex)
        {
            return ex is AccessForbiddenException;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

        public ErrorResult Handle(Exception ex)
        {
            var validationException = (AccessForbiddenException)ex;

            var error = new ErrorProperty(nameof(AccessForbiddenException), validationException.Message);

            return new ErrorResult(error);
        }
    }
}