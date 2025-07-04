namespace CLUK.Skeleton.API.Query.Policies.Models;

public record PolicyResponse(ProductType ProductType, IEnumerable<Client> Clients)
{
    public static PolicyResponse CreateNoContentResponse()
    {
        return new PolicyResponse(ProductType.UNKNW, Enumerable.Empty<Client>());
    }
}