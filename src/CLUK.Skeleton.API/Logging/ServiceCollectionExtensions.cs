using CLUK.Skeleton.API.Logging.Rules;

using Microsoft.AspNetCore.HttpLogging;

namespace CLUK.Skeleton.API.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpLogging(this IServiceCollection services)
    {
        /*
         * This logging will appear under the 'Category' of 'Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware'
         *
         * But only if "Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware" is set to
         * "Information" or higher in the logging config.
         */
        services.AddHttpLogging(o =>
        {
            o.LoggingFields = HttpLoggingFields.Duration | HttpLoggingFields.RequestPropertiesAndHeaders;
            o.CombineLogs = true;
            o.RequestHeaders.Add("X-AppGW-Trace-Id");
        });

        services.AddHttpLoggingInterceptor<FilterHttpLoggingBasedOnPathInterceptor>();

        AddLogExclusionRulesInOrderOfExecution();

        return services;

        void AddLogExclusionRulesInOrderOfExecution()
        {
            services.AddTransient<IHttpLoggingInterceptorRule, DoNotLogCertainFileTypesRule>();

            /*
             * This rule considers many things, so any rules that can more readily rule a request out
             * should appear beforehand to save that 'effort'.
             */
            services.AddTransient<IHttpLoggingInterceptorRule, DoNotLogKeepAliveRequestsRule>();
        }
    }

    public static void AddHttpLogging(this IApplicationBuilder app)
    {
        /*
         * The position of this after 'UseStaticFiles' is important as it means requests for static files
         * will not be logged.
         */

        app.UseHttpLogging();
    }
}