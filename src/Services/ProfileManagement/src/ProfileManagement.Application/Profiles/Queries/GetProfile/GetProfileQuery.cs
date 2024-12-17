namespace ProfileManagement.Application.Profiles.Queries.GetProfile;

public record GetProfileQuery(Guid Id) : IQuery<ProfileResponse>;