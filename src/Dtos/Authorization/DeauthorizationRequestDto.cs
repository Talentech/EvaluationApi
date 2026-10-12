using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Authorization
{
    /// <summary>
    /// Sent to your deauthorize endpoint in Fixed token mode when a customer disconnects the integration.
    /// Refresh token apps get a form post instead (token, token_type_hint=refresh_token, HTTP Basic client_id:client_secret).
    /// We have already deleted our stored token, and your response does not change anything on our side.
    /// </summary>
    public class DeauthorizationRequestDto
    {
        /// <summary>
        /// The customer's admin user who disconnected the integration. EncryptedFields is always empty here.
        /// </summary>
        public TriggeredByDto TriggeredBy { get; set; }

        /// <summary>
        /// The stored response of your token endpoint, exactly as you returned it.
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
