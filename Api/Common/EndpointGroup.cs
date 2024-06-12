namespace Api.Common;

public abstract class EndpointGroup
{
    public abstract void Map(IEndpointRouteBuilder builder);
}
