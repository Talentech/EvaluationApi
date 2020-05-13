namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Config
{
    public class EvaluationApiConfig
    {
        public string EvaluationApiBaseUrl { get; set; }
        public const string ResultsEndpointExtension = "/api/v1/results/{invitationId}";

        public string ResultsEndpoint(string invitationId)
        {
            return EvaluationApiBaseUrl.TrimEnd('/') + ResultsEndpointExtension.Replace("{invitationId}", invitationId);
        }
    }
}
