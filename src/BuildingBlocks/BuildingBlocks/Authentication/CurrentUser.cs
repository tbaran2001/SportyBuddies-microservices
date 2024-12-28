namespace BuildingBlocks.Authentication;

public record CurrentUser(Guid Id, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}