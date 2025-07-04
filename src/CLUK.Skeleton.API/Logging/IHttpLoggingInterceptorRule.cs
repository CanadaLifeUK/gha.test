using Microsoft.AspNetCore.HttpLogging;

namespace CLUK.Skeleton.API.Logging;

public interface IHttpLoggingInterceptorRule
{
    bool RuleForNotLogging(HttpLoggingInterceptorContext context);
}