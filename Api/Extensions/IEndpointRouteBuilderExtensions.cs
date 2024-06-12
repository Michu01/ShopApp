using Api.Common;

using System.Reflection;

namespace Api.Extensions;

public static class IEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpointGroupType = typeof(EndpointGroup);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroup group)
            {
                var groupName = group.GetType().Name;

                var groupBuilder = builder
                    .MapGroup(groupName)
                    .WithTags(groupName);

                group.Map(groupBuilder);
            }
        }

        return builder;
    }
}
