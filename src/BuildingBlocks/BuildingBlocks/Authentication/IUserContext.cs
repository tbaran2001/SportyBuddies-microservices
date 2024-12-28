namespace BuildingBlocks.Authentication;

public interface IUserContext
{
    CurrentUser GetCurrentUser();
}