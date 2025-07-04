namespace CLUK.Skeleton.API.Query.Policies;

public interface IPoliciesService
{
    Task<PolicyResponse> GetClientsByPolicyNumber(string policyNumber);
}