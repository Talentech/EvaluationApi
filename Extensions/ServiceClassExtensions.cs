using Everest.SampleAssessment.AssessmentProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Everest.SampleAssessment.Extensions
{
    public static class ServiceClassExtensions
    {
        public static void AddHttpClient(this IServiceCollection services)
        {
            
            services.AddHttpClient<ApiHttpClient>();
        }
    }
}
