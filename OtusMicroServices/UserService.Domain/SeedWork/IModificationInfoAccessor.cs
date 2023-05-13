namespace UserService.Domain.SeedWork
{
    public interface IModificationInfoAccessor
    {
        void SetCreated(DateTime now, Guid createdBy);
        void SetModified(DateTime now, Guid modifiedBy);
    }
}