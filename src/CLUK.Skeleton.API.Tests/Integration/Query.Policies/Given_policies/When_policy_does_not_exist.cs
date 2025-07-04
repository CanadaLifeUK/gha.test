namespace CLUK.Skeleton.API.Tests.Integration.Query.Policies.Given_policies;

public class When_policy_does_not_exist : BaseControllerTests
{
    [Fact]
    public async Task It_should_return_no_content_status_code()
    {
        var policyNumber = "45678123";
        var databasePolicies = new List<ClientEntity>()
        {
            TestClientEntity.CreateAsPolicyHolder(policyNumber)
        };

        await InsertRecords(databasePolicies);

        var response = await GetAsync($"policies/{policyNumber.Remove(1)}");

        Can_verify_response_received_with_http_status_code(response, HttpStatusCode.NoContent);
    }
}