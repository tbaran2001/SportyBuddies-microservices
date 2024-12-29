using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Domain.Events;

public record ProfileSportRemovedEvent(Guid ProfileId, Guid SportId) : IDomainEvent;