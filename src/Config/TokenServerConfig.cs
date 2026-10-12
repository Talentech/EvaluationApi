namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Config
{
    public class TokenServerConfig
    {
        /// <summary>
        /// The token endpoint Talentech gives you, together with the client id and client secret.
        /// </summary>
        public string TokenEndpoint { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
