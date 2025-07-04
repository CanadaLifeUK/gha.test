namespace CLUK.Skeleton.API.Tests.Integration.Query.Policies.Given_policies;

public class When_policy_not_inforce : BaseControllerTests
{
    [Fact]
    public async Task It_should_return_no_content_status_code()
    {
        var policyNumber = "67812345";
        var databasePolicies = new List<ClientEntity>()
        {
            TestClientEntity.CreateAsNotInforce(policyNumber)
        };

        await InsertRecords(databasePolicies);

        var response = await GetAsync($"policies/{policyNumber}");

        Can_verify_response_received_with_http_status_code(response, HttpStatusCode.NoContent);
    }
}