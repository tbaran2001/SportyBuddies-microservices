using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Domain.Events;

public class ProfileCreatedEvent(Profile profile) : IDomainEvent
{

}