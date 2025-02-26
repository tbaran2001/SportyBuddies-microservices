using BuildingBlocks.Test;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProfileManagement.API.Data;

namespace ProfileManagement.IntegrationTests;

public class ProfileServiceIntegrationTestBase :
    IntegrationTestBase<IntegrationTestWebAppFactory, Program, ApplicationDbContext>
{
    protected ProfileServiceIntegrationTestBase(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
}