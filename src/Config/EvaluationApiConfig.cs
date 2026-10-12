namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Config
{
    public class EvaluationApiConfig
    {
        /// <summary>
        /// The Evaluation API base URL Talentech gives you, for example https://&lt;host&gt;/api. /v1/results is appended to it.
        /// </summary>
        public string EvaluationApiBaseUrl { get; set; }
        public const string ResultsEndpointExtension = "/v1/results";

        public string ResultsEndpoint => EvaluationApiBaseUrl.TrimEnd('/') + ResultsEndpointExtension;
    }
}
