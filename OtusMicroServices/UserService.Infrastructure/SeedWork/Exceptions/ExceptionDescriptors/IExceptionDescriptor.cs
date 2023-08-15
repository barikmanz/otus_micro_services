using System.Net;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public interface IExceptionDescriptor
    {
        bool CanHandle(Exception ex);

        HttpStatusCode StatusCode { get; }

        ErrorResult Handle(Exception ex);
    }
}
