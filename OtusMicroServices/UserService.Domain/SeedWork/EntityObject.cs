namespace UserService.Domain.SeedWork
{
    public abstract class EntityObject<T, TKey> : IEquatable<T>, IModificationInfoAccessor
        where T : EntityObject<T, TKey>
    {
        public abstract TKey GetKey();

        public bool Equals(T other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return ReferenceEquals(this, other) || GetKey().Equals(other.GetKey());
        }

        public override bool Equals(object obj)
        {
            return obj is T other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(0, GetKey());
        }

        public DateTime Created { get; private set; }
        public Guid CreatedById { get; private set; }
        public DateTime Modified { get; private set; }
        public Guid ModifiedById { get; private set; }

        void IModificationInfoAccessor.SetCreated(DateTime now, Guid createdBy)
        {
            Created = now;
            CreatedById = createdBy;
            Modified = now;
            ModifiedById = createdBy;
        }

        void IModificationInfoAccessor.SetModified(DateTime now, Guid modifiedBy)
        {
            Modified = now;
            ModifiedById = modifiedBy;
        }
    }
}
