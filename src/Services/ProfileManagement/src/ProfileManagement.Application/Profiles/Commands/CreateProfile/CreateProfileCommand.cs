using BuildingBlocks.CQRS;
using MediatR;
using ProfileManagement.Application.DTOs;

namespace ProfileManagement.Application.Profiles.Commands.CreateProfile;

public record CreateProfileCommand(string Name, string Description) : ICommand<ProfileResponse>;