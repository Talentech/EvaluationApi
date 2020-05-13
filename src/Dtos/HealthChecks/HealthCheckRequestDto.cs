using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.HealthChecks
{
    public class HealthCheckRequestDto
    {
        /// <summary>
        /// The schema for this is controlled by the Partner. The contents here is identical to what is returned from the OAuth token endpoint.
        /// </summary>
        public ExamplePartnerTokenDto ProviderConfiguration { get; set; }
    }
}
