using System.Reflection;
using BuildingBlocks.Core.Event;
using FluentAssertions;
using NetArchTest.Rules;

namespace ProfileManagement.ArchitectureTests;

public class DomainTests
{
    private readonly Assembly _assembly = typeof(Program).Assembly;
    
    private IEnumerable<string> GetModelNamespaces()
    {
        return _assembly.GetTypes()
            .Select(t => t.Namespace)
            .Where(ns => ns != null && ns.EndsWith(".Models"))
            .Distinct()!;
    }

    [Fact]
    public void DomainModels_Should_Have_A_Static_Factory_Method()
    {
        var modelNamespaces = GetModelNamespaces();

        var types = new List<Type>();
        foreach (var ns in modelNamespaces)
        {
            types.AddRange(Types.InAssembly(_assembly)
                .That()
                .ResideInNamespace(ns)
                .And()
                .AreClasses()
                .GetTypes());
        }

        foreach (var type in types)
        {
            var staticMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
            bool hasFactoryMethod = staticMethods.Any(m => m.Name.StartsWith("Create"));
            
            hasFactoryMethod.Should().BeTrue();
        }
    }
    
    [Fact]
    public void DomainEvents_Should_ResideInCorrectNamespace()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .ResideInNamespace("ProfileManagement.API.Profiles.Features.Commands")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void DomainEvents_Should_HaveDomainEventPostfix()
    {
        var result = Types.InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
}