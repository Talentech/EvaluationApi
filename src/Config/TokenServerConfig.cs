namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Config
{
    public class TokenServerConfig
    {
        /// <summary>
        /// The Talentech token endpoint, given to you by Talentech together with the client id and client secret.
        /// </summary>
        public string TokenEndpoint { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
