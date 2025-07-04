namespace CLUK.Skeleton.API.Query.Policies;

public class PoliciesService(ILogger<PoliciesService> logger, IDatabaseClient databaseClient) : IPoliciesService
{
    public async Task<PolicyResponse> GetClientsByPolicyNumber(string policyNumber)
    {
        try
        {
            var clientEntities = await databaseClient.Query<ClientEntity>(Constants.GetClientsByPolicyNumberSql, new { policyNumber });

            if (clientEntities is not null && clientEntities?.Any() is false)
            {
                return PolicyResponse.CreateNoContentResponse();
            }

            if (IsPolicyHolderBusiness(clientEntities!))
            {
                logger.LogWarning("Organisation policy owner not supported");
                return PolicyResponse.CreateNoContentResponse();
            }

            if (IfDoesNotContainPolicyHolders(clientEntities!))
            {
                logger.LogWarning("Policy Owner type not supported");
                return PolicyResponse.CreateNoContentResponse();
            }

            var mappedClients = clientEntities!
                .Where(ClientIsValid)
                .Select(x => MapToClient(x))
                .ToList() ?? Enumerable.Empty<Models.Client>();

            return new PolicyResponse(GetProductType(clientEntities!), mappedClients);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while querying client entities.");
            return PolicyResponse.CreateNoContentResponse();
        }

        bool ClientIsValid(ClientEntity clientEntity)
        {
            return clientEntity is not null &&
                   clientEntity.IsBusiness is false &&
                   clientEntity.InForcePolicy &&
                   clientEntity.IsAlive;
        }

        bool IsPolicyHolderBusiness(IEnumerable<ClientEntity> clientEntities)
        {
            return clientEntities.Any(i => i.IsBusiness && i.IsPolicyOwner);
        }

        bool IfDoesNotContainPolicyHolders(IEnumerable<ClientEntity> clientEntities)
        {
            return clientEntities.Count(i => i.IsPolicyOwner) == 0;
        }

        Models.Client MapToClient(ClientEntity clientEntity)
        {
            var firstName = string.Empty;
            var surname = string.Empty;

            if (string.IsNullOrWhiteSpace(clientEntity.ClientName) is false)
            {
                var splitClientName = clientEntity.ClientName.Split(';');

                surname = splitClientName[0];
                firstName = splitClientName[1];
            }

            var clientReference = $"0000000{clientEntity.ClientReference}";

            return new Models.Client(
                      Guid.NewGuid().ToString(),
                      firstName,
                      surname,
                      clientEntity.DateOfBirth,
                      clientEntity.EmailAddress,
                      clientEntity.PostCode,
                      clientReference[^7..],
                      MailingType.NotSpecified);
        }

        ProductType GetProductType(IEnumerable<ClientEntity> clientEntities)
        {
            var productType = clientEntities.First().PlanCode;

            if (Enum.TryParse(typeof(ProductType), productType, out var mappedProductType))
            {
                return (ProductType)mappedProductType;
            }

            return ProductType.UNKNW;
        }
    }
}