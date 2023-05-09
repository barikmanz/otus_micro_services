namespace UserService.Domain.SeedWork
{
    public abstract class DomainEvent
    {
        public long EventId { get; private set; }

        public DateTimeOffset Created { get; private set; }

        public Guid MessageKey { get; private set; }

        public bool ShouldSerializeEventId() => false;
        public bool ShouldSerializeCreated() => false;
        public bool ShouldSerializeMessageKey() => false;

        protected DomainEvent(DateTimeOffset created, Guid messageKey)
        {
            Created = created;
            MessageKey = messageKey;
        }
    }
}