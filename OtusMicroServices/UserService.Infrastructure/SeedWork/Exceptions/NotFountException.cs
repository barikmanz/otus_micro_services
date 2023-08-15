namespace UserService.Infrastructure.SeedWork.Exceptions
{
    public class NotFoundException : UnauthorizedAccessException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}