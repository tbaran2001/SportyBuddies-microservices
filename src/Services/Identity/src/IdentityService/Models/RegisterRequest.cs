﻿namespace IdentityService.Models;

public class RegisterRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Name { get; init; }
}