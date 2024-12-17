using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Domain.Events;

public class ProfileSportRemovedEvent(Profile profile) : IDomainEvent
{
}