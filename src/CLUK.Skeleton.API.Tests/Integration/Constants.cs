using System.Text.Json.Serialization;

namespace CLUK.Skeleton.API.Tests.Integration;

internal static class Constants
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IgnoreReadOnlyProperties = false,
        IgnoreReadOnlyFields = false,
        IncludeFields = true
    };

    public const string DatabaseInitialisationSql = """
        SET ANSI_NULLS ON
        SET QUOTED_IDENTIFIER ON

        CREATE TABLE [dbo].[BP_BasicPolicy](
        	[PolicyNumber] [char](8) NOT NULL,
        	[PolicyStatus] [char](1) NOT NULL,
        	[PlanCode] [char](5) NOT NULL,
        	CONSTRAINT [pk_BP] PRIMARY KEY CLUSTERED ([PolicyNumber] ASC)
        )

        CREATE TABLE [dbo].[LN_LifeName](
        	[PolicyNumber] [char](8) NOT NULL,
        	[ClientReference] [int] NOT NULL,
        	[DateOfBirth] [date] NOT NULL,
        	[IfLifeAssured] [char](1) NOT NULL,
        	[IfPolicyOwner] [char](1) NOT NULL,
        	CONSTRAINT [pk_LN] PRIMARY KEY CLUSTERED ([PolicyNumber] ASC, [ClientReference] ASC)
        )

        CREATE TABLE [dbo].[CE_Client](
        	[ClientReference] [int] NOT NULL,
        	[ClientName1] [char](35) NOT NULL,
        	[PersonalBusinessInd] [char](1) NOT NULL,
        	[PostCode] [char](8) NOT NULL,
        	[ClientStatus] [char](1) NOT NULL,
        	CONSTRAINT [pk_CE] PRIMARY KEY CLUSTERED ([ClientReference] ASC)
        )

        CREATE TABLE [dbo].[NT_GeneralPurposeNote](
        	[ClientReference] [int] NOT NULL,
        	[NoteReference] [int] NOT NULL,
        	[TypeOfNote] [char](2) NOT NULL,
        	[NoteDetail1] [char](30) NOT NULL,
        	[NoteDetail2] [char](30) NOT NULL,
        	CONSTRAINT [pk_NT] PRIMARY KEY CLUSTERED ([ClientReference] ASC, [NoteReference] ASC)
        )
        """;

    public const string InsertSql = """
        INSERT INTO [dbo].[BP_BasicPolicy]([PolicyNumber], [PolicyStatus], [PlanCode])
        	VALUES (@PolicyNumber, @PolicyStatus, @PlanCode)
        """;

    public const string InsertClientSql = """
        INSERT INTO [dbo].[LN_LifeName]([PolicyNumber], [ClientReference], [DateOfBirth], [IfLifeAssured], [IfPolicyOwner])
        	VALUES (@PolicyNumber, @ClientReference, @DateOfBirth, @LifeAssuredInd, @PolicyOwnerInd)

        INSERT INTO [dbo].[CE_Client]([ClientReference], [ClientName1], [PersonalBusinessInd], [PostCode], [ClientStatus])
        	VALUES (@ClientReference, @ClientName, @PersonalBusinessInd, @PostCode, @ClientStatus)

        INSERT INTO [dbo].[NT_GeneralPurposeNote]([ClientReference], [NoteReference], [TypeOfNote], [NoteDetail1], [NoteDetail2])
        	VALUES (@ClientReference, 1,'E', REPLACE(REPLACE(@ClientName, ';', '.'), '...',''), '@email.com')
        """;
}