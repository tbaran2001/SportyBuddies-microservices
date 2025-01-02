using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Domain.Events;

public record ProfileUpdatedEvent(Guid ProfileId) : IDomainEvent;
