namespace UserService.Domain.SeedWork
{
    public interface IDomainEventSource
    {
        IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    }
}