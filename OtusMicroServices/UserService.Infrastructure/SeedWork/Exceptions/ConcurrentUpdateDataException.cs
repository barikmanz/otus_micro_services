namespace UserService.Infrastructure.SeedWork.Exceptions
{
    public class ConcurrentUpdateDataException : InternalInvalidOperationException
    {
        public ConcurrentUpdateDataException(string message) : base(message)
        {
        }
    }
}