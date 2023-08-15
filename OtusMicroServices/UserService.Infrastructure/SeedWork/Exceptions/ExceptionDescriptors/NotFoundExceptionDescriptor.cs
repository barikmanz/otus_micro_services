using System.Net;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public class NotFoundExceptionDescriptor : IExceptionDescriptor
    {
        public bool CanHandle(Exception ex)
        {
            return ex is NotFoundException;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public ErrorResult Handle(Exception ex)
        {
            var validationException = (NotFoundException)ex;

            var error = new ErrorProperty(nameof(NotFoundException), validationException.Message);

            return new ErrorResult(error);
        }
    }
}
