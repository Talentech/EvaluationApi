namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Config
{
    public class EvaluationApiConfig
    {
        public string EvaluationApiBaseUrl { get; set; }
        public const string ResultsEndpointExtension = "/v1/results";

        public string ResultsEndpoint => EvaluationApiBaseUrl.TrimEnd('/') + ResultsEndpointExtension;
    }
}
