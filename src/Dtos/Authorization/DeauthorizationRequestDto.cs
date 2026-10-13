using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Authorization
{
    /// <summary>
    /// Sent to your deauthorize endpoint in Fixed token mode when a customer disconnects the integration.
    /// Refresh token apps get a form post instead (token, token_type_hint=refresh_token, HTTP Basic client_id:client_secret).
    /// The response is not used.
    /// </summary>
    public class DeauthorizationRequestDto
    {
        /// <summary>
        /// The customer's admin user who disconnected the integration.
        /// </summary>
        public TriggeredByDto TriggeredBy { get; set; }

        /// <summary>
        /// The same JSON your token endpoint returned.
        /// </summary>
        public ExamplePartnerTokenDto Auth { get; set; }

        /// <summary>
        /// The client id given to Talentech by the partner
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The client secret given to Talentech by the partner
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
