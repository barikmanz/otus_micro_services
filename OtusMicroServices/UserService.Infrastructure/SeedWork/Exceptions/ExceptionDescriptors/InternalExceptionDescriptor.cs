using System.Net;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public class InternalExceptionDescriptor : IExceptionDescriptor
    {
        public bool CanHandle(Exception ex)
        {
            return ex is InternalException;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public ErrorResult Handle(Exception ex)
        {
            var validationException = (InternalException)ex;

            var error = new ErrorProperty(nameof(InternalException), validationException.Message);

            return new ErrorResult(error);
        }
    }
}
