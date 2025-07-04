namespace CLUK.Skeleton.API.Tests.Integration.Query.Policies.Given_policies;

public class When_policy_holder_business : BaseControllerTests
{
    [Fact]
    public async Task It_should_return_no_content_status_code()
    {
        var policyNumber = "56781234";
        var databasePolicies = new List<ClientEntity>()
        {
            TestClientEntity.CreateAsBusinessPolicyHolder(policyNumber)
        };

        await InsertRecords(databasePolicies);

        var response = await GetAsync($"policies/{policyNumber}");

        Can_verify_response_received_with_http_status_code(response, HttpStatusCode.NoContent);
    }
}