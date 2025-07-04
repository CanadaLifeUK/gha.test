using Microsoft.AspNetCore.HttpLogging;

namespace CLUK.Skeleton.API.Logging;

internal sealed class FilterHttpLoggingBasedOnPathInterceptor(
    ILogger<FilterHttpLoggingBasedOnPathInterceptor> logger,
    IEnumerable<IHttpLoggingInterceptorRule> rules)
    : IHttpLoggingInterceptor
{
    private readonly ValueTask logAsPerConfig = default;

    public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
    {
        if (rules.Any(s => s.RuleForNotLogging(logContext)))
        {
            logger.LogTrace("A rule resulted in no logging being required");

            logContext.LoggingFields = HttpLoggingFields.None;
        }

        return logAsPerConfig;
    }

    public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext)
    {
        return logAsPerConfig;
    }
}