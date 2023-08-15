namespace UserService.Infrastructure.SeedWork.Exceptions
{
    public class AccessForbiddenException : UnauthorizedAccessException
    {
        public AccessForbiddenException(string message)
            : base(message)
        {
        }

        public AccessForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
