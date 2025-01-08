
using BuildingBlocks.Exceptions;

namespace ProfileManagement.API.Profiles.Exceptions;

public class ProfileNotFoundException(Guid id) : NotFoundException("Profile", id);