﻿namespace Framework.Domain;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> DomainEvents { get;}
    void AddDomainEvents(IDomainEvent @event);
    IReadOnlyList<IDomainEvent> ClearDomainEvents();
}