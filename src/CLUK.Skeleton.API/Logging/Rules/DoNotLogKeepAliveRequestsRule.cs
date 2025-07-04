using Microsoft.AspNetCore.HttpLogging;

namespace CLUK.Skeleton.API.Logging.Rules;

public class DoNotLogKeepAliveRequestsRule(ILogger<DoNotLogKeepAliveRequestsRule> logger) : IHttpLoggingInterceptorRule
{
    public bool RuleForNotLogging(HttpLoggingInterceptorContext context)
    {
        if (string.Equals(context.HttpContext.Request.Headers.Connection, "keep-alive", StringComparison.InvariantCultureIgnoreCase) is false)
        {
            logger.LogTrace("Connection is not keep-alive");

            return false;
        }

        if (string.Equals(context.HttpContext.Request.Path, "/", StringComparison.InvariantCultureIgnoreCase) is false)
        {
            logger.LogTrace("Path is not /");

            return false;
        }

        if (string.IsNullOrEmpty(context.HttpContext.Request.Headers.Accept) is false)
        {
            logger.LogTrace("Accept has values");

            return false;
        }

        logger.LogTrace("Request looks like a keep alive - ineligible for logging");

        return true;
    }
}