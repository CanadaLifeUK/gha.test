namespace CLUK.Skeleton.API.Query.Policies.Models;

public record ClientEntity(
    string PolicyNumber,
    string PlanCode,
    string PolicyStatus,
    int ClientReference,
    string ClientName,
    DateTime DateOfBirth,
    string ClientStatus,
    string LifeAssuredInd,
    string PolicyOwnerInd,
    string PostCode,
    string EmailAddress,
    string PersonalBusinessInd)
{
    public bool IsPolicyOwner => Constants.PolicyHolderFlag.Contains(PolicyOwnerInd);

    public bool IsLifeAssured => Constants.LifeAssuredFlag.Contains(LifeAssuredInd);

    public bool IsBusiness => PersonalBusinessInd.Equals(Constants.PersonalCode, StringComparison.OrdinalIgnoreCase) is false;

    public bool IsAlive => ClientStatus.Equals(Constants.AliveClientStatus, StringComparison.OrdinalIgnoreCase);

    public bool InForcePolicy => Constants.PolicyStatusFlag.Contains(PolicyStatus);
}