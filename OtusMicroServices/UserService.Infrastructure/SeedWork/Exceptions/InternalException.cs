namespace UserService.Infrastructure.SeedWork.Exceptions
{
    public class InternalException : ApplicationException
    {
        public InternalException(string message)
            : base(message)
        {
        }

        public InternalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
