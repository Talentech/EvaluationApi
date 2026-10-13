namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.AccessTokens
{
    /// <summary>
    /// The form we post to your token endpoint. With grant_type=authorization_code it carries code and redirect_uri.
    /// With grant_type=refresh_token (Refresh token mode) it carries refresh_token instead.
    /// </summary>
    public class TokenRequestDto
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
        public string? code { get; set; }
        public string? redirect_uri { get; set; }
        public string? refresh_token { get; set; }
    }
}
