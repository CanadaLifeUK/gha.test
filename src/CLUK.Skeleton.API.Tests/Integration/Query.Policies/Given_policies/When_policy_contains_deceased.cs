namespace CLUK.Skeleton.API.Tests.Integration.Query.Policies.Given_policies;

public class When_policy_contains_deceased : BaseControllerTests
{
    [Fact]
    public async Task It_should_return_expected()
    {
        var policyNumber = "23456781";
        var databasePolicies = new List<ClientEntity>()
        {
            TestClientEntity.CreateAsPolicyHolder(policyNumber),
            TestClientEntity.CreateAsNonPolicyHolder(policyNumber),
            TestClientEntity.CreateDeceased(policyNumber)
        };

        await InsertRecords(databasePolicies);

        var response = await GetAsync($"policies/{policyNumber}");

        Can_verify_response_received_with_http_status_code(response);
        var policyResponse = (await Can_verify_and_get_content<PolicyResponse>(response))!;

        policyResponse.Should().NotBeNull();
        policyResponse.Clients.Should().NotBeNull();
        policyResponse.Clients.Count().Should().Be(2);

        policyResponse.Clients.Count(f => databasePolicies[0].ClientReference.ToString().Contains($"{f.ExternalReferenceNumber}")).Should().Be(1);
        policyResponse.Clients.Count(f => databasePolicies[1].ClientReference.ToString().Contains($"{f.ExternalReferenceNumber}")).Should().Be(1);
        policyResponse.Clients.Count(f => databasePolicies[2].ClientReference.ToString().Contains($"{f.ExternalReferenceNumber}")).Should().Be(0);

        policyResponse.Clients.Count(f => databasePolicies[0].ClientName.Contains($"{f.Surname};{f.FirstName}")).Should().Be(1);
        policyResponse.Clients.Count(f => databasePolicies[1].ClientName.Contains($"{f.Surname};{f.FirstName}")).Should().Be(1);
        policyResponse.Clients.Count(f => databasePolicies[2].ClientName.Contains($"{f.Surname};{f.FirstName}")).Should().Be(0);
    }
}
