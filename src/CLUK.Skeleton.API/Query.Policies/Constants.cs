namespace CLUK.Skeleton.API.Query.Policies;

public static class Constants
{
    public const string PersonalCode = "P";
    public const string AliveClientStatus = "A";

    public const string InforcePolicyStatus = "I";
    public const string OtherPolicyStatus = "O";

    public const string LifeAssured = "Y";
    public const string SingleLifeAssured = "S";
    public const string NotLifeAssured = "N";
    public const string JointLifeAssured = "J";
    public const string OtherLifeAssured = "O";
    public const string DeceasedLifeAssured = "D";
    public const string InvalidJointLifeAssured = "I";

    public const string GetClientsByPolicyNumberSql = @"
SELECT		BP.[PolicyNumber],
			BP.[PlanCode],
			BP.[PolicyStatus],
			LN.[ClientReference],
			CE.[ClientName1] AS [ClientName],
			LN.[DateOfBirth],
			CE.[ClientStatus],
			LN.[IfLifeAssured] AS [LifeAssuredInd], 
			LN.[IfPolicyOwner] AS [PolicyOwnerInd],
			CE.[PostCode],
			GPN.[NoteDetail1]+GPN.[NoteDetail2] AS [EmailAddress],
			CE.[PersonalBusinessInd]

FROM		[dbo].[BP_BasicPolicy] AS BP
INNER JOIN	[dbo].[LN_LifeName] AS LN ON BP.[PolicyNumber] = LN.[PolicyNumber]
INNER JOIN	[dbo].[CE_Client] AS CE ON LN.[ClientReference] = CE.[ClientReference]
LEFT OUTER JOIN (SELECT		MAX([NoteReference]) AS [NoteReference],
							[ClientReference]
				FROM		[dbo].[NT_GeneralPurposeNote]
				WHERE		[TypeOfNote] = 'E'
				GROUP BY	[ClientReference]) AS NR ON LN.[ClientReference] = NR.[ClientReference]
LEFT OUTER JOIN	[dbo].[NT_GeneralPurposeNote] AS GPN ON LN.[ClientReference] = GPN.[ClientReference] AND NR.[NoteReference] = GPN.[NoteReference]
WHERE		BP.[PolicyNumber] = @policyNumber";

    public static readonly string[] PolicyStatusFlag = { InforcePolicyStatus, OtherPolicyStatus };

    public static readonly string[] PolicyHolderFlag = { "Y", "S", "J" };

    public static readonly string[] LifeAssuredFlag = { LifeAssured, SingleLifeAssured, JointLifeAssured, OtherLifeAssured };
}