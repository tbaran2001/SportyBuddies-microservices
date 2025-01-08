﻿using BuildingBlocks.Messaging.Events.ProfileManagement;
using MassTransit;
using MediatR;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Features.Commands.AddProfileSport;
using ProfileManagement.API.Profiles.Features.Commands.CreateProfile;

namespace ProfileManagement.API.Profiles.EventHandlers.Domain;

public class ProfileSportAddedEventHandler(
    IProfilesRepository profilesRepository,
    IPublishEndpoint publishEndpoint,
    ILogger<ProfileSportAddedEventHandler> logger)
    : INotificationHandler<ProfileSportAddedDomainEvent>
{
    public async Task Handle(ProfileSportAddedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var potentialMatches =
            await profilesRepository.GetPotentialMatchesAsync(domainEvent.ProfileId, domainEvent.SportIds);

        var profileSportAddedIntegrationEvent = new ProfileSportAddedIntegrationEvent
        {
            ProfileId = domainEvent.ProfileId,
            PotentialMatches = potentialMatches
        };
        await publishEndpoint.Publish(profileSportAddedIntegrationEvent, cancellationToken);
    }
}