
namespace ProfileManagement.Application.Exceptions;

public class ProfileNotFoundException(Guid id) : NotFoundException("Profile", id);