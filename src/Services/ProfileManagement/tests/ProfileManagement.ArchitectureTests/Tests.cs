using System.Reflection;
using BuildingBlocks.CQRS;
using Carter;
using FluentAssertions;
using NetArchTest.Rules;

namespace ProfileManagement.ArchitectureTests;

public class Tests
{
    private readonly Assembly _assembly = typeof(Program).Assembly;

    [Fact]
    public void CommandHandlers_Should_ResideInCorrectNamespace()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .ResideInNamespace("ProfileManagement.API.Profiles.Features.Commands")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void CommandHandlers_Should_HaveCorrectName()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void QueryHandlers_Should_ResideInCorrectNamespace()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .ResideInNamespace("ProfileManagement.API.Profiles.Features.Queries")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void QueryHandlers_Should_HaveCorrectName()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Endpoints_Should_NotDependOnRepositories()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(ICarterModule))
            .ShouldNot()
            .HaveDependencyOn("ProfileManagement.API.Profiles.Repositories")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainModels_Should_NotDependOnApplicationServices()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ResideInNamespace("ProfileManagement.API.Profiles.Models")
            .ShouldNot()
            .HaveDependencyOn("ProfileManagement.API.Profiles.Repositories")
            .And()
            .HaveDependencyOn("ProfileManagement.API.Profiles.Services")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
}