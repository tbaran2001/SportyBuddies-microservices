namespace IdentityService.Identity;

public static class Constants
{
    public static class Role
    {
        public const string Admin = "admin";
        public const string User = "user";
    }

    public static class StandardScopes
    {
        public const string Roles = "roles";
        public const string ProfileManagementApi = "profilemanagement-api";
        public const string SportApi = "sport-api";
        public const string MatchingApi = "matching-api";
        public const string BuddiesApi = "buddies-api";
        public const string IdentityApi = "identity-api";
    }
}