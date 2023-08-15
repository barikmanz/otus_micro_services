namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public sealed class ErrorResult
    {
        public bool Ok { get; }
        public ErrorProperty[]? Errors { get; }

        public ErrorResult(params ErrorProperty[]? errors)
        {
            Ok = false;
            Errors = errors;
        }
    }
}