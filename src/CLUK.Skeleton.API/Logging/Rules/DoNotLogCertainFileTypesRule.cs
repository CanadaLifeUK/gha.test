using Microsoft.AspNetCore.HttpLogging;

namespace CLUK.Skeleton.API.Logging.Rules;

public class DoNotLogCertainFileTypesRule(ILogger<DoNotLogCertainFileTypesRule> logger) : IHttpLoggingInterceptorRule
{
    private readonly string[] fileTypeRequestsWeDoNotWantToLog =
        [];

    public bool RuleForNotLogging(HttpLoggingInterceptorContext context)
    {
        return DetermineIfTheRequestIsForAFileTypeWeIgnore(context.HttpContext.Request.Path);
    }

    private bool DetermineIfTheRequestIsForAFileTypeWeIgnore(PathString requestPath)
    {
        string incomingRequestExtension = Path.GetExtension(requestPath);

        bool shouldWeNotLog = fileTypeRequestsWeDoNotWantToLog.Any(a => string.Equals(a, incomingRequestExtension, StringComparison.InvariantCultureIgnoreCase));

        logger.LogTrace($"Incoming file extension: [{incomingRequestExtension}] resulted in: [{shouldWeNotLog}]");

        return shouldWeNotLog;
    }
}