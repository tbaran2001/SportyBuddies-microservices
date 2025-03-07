using System.Reflection;
using BuildingBlocks.CQRS;
using FluentAssertions;
using NetArchTest.Rules;

namespace ProfileManagement.ArchitectureTests;

public class Tests
{
    private readonly Assembly _assembly = typeof(Program).Assembly;

    [Fact]
    public void CommandHandlers_Should_Reside_In_CorrectNamespace()
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
    public void CommandHandlers_Should_Have_CorrectName()
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
    public void QueryHandlers_Should_Reside_In_CorrectNamespace()
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
    public void QueryHandlers_Should_Have_CorrectName()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
}