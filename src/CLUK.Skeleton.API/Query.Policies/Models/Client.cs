namespace CLUK.Skeleton.API.Query.Policies.Models;

public record Client(
    string Id,
    string FirstName,
    string Surname,
    DateTime DateOfBirth,
    string EmailAddress,
    string PostCode,
    string ExternalReferenceNumber,
    MailingType MailingIndicator);