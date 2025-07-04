namespace CLUK.Skeleton.API.Tests.TestDoubles;

public class TestClientEntity : FakerForConstructorInitialisedTypes<ClientEntity>
{
    public TestClientEntity(
        string policyNumber,
        bool aliveClient,
        bool policyHolder,
        bool businessOnly,
        bool inforcePolicyOnly)
    {
        RuleFor(r => r.ClientName, s => $"{s.Person.LastName};{s.Person.FirstName};;;");
        RuleFor(r => r.DateOfBirth, s => s.Person.DateOfBirth);
        RuleFor(r => r.ClientReference, s => s.Random.Int(1000000));
        RuleFor(r => r.EmailAddress, s => $"{s.Person.LastName}.{s.Person.FirstName}@email.com");
        RuleFor(r => r.PlanCode, s => s.PickRandom(new string[] { $"{ProductType.FDP}", $"{ProductType.FTI}", $"{ProductType.PIP}" }));
        RuleFor(r => r.PolicyNumber, policyNumber);
        RuleFor(r => r.PostCode, s => s.Address.ZipCode("??## #??"));

        if (policyHolder)
        {
            RuleFor(r => r.PolicyOwnerInd, s => s.PickRandom(Query.Policies.Constants.PolicyHolderFlag));
        }
        else
        {
            RuleFor(r => r.PolicyOwnerInd, "N");
        }

        RuleFor(r => r.LifeAssuredInd, s => s.PickRandom(Query.Policies.Constants.LifeAssuredFlag));

        if (aliveClient)
        {
            RuleFor(r => r.ClientStatus, s => s.PickRandom(new string[] { Query.Policies.Constants.AliveClientStatus }));
        }
        else
        {
            RuleFor(r => r.ClientStatus, s => s.PickRandom(new string[] { "D" }));
        }

        if (inforcePolicyOnly)
        {
            RuleFor(r => r.PolicyStatus, s => s.PickRandom(Query.Policies.Constants.InforcePolicyStatus));
        }
        else
        {
            RuleFor(r => r.PolicyStatus, s => s.PickRandom(new string[] { "D" }));
        }

        if (businessOnly)
        {
            RuleFor(r => r.PersonalBusinessInd, s => s.PickRandom(new string[] { "C", "X" }));
        }
        else
        {
            RuleFor(r => r.PersonalBusinessInd, s => s.PickRandom(new string[] { Query.Policies.Constants.PersonalCode }));
        }
    }

    public static TestClientEntity CreateAsBusinessPolicyHolder(string policyNumber)
    {
        return new TestClientEntity(policyNumber, true, true, true, true);
    }

    public static TestClientEntity CreateAsPolicyHolder(string policyNumber)
    {
        return new TestClientEntity(policyNumber, true, true, false, true);
    }

    public static TestClientEntity CreateAsNonPolicyHolder(string policyNumber)
    {
        return new TestClientEntity(policyNumber, true, false, false, true);
    }

    public static TestClientEntity CreateDeceased(string policyNumber)
    {
        return new TestClientEntity(policyNumber, false, false, false, true);
    }

    public static TestClientEntity CreateAsNotInforce(string policyNumber)
    {
        return new TestClientEntity(policyNumber, true, true, false, false);
    }
}