namespace ProfileManagement.API.Profiles.Exceptions;

public class ProfileAlreadyExistException() : ConflictException("Profile already exist!")
{
}