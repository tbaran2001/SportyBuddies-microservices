using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Domain.Events;

public record ProfileSportAddedEvent(Guid ProfileId, IEnumerable<Guid> SportIds) : IDomainEvent;