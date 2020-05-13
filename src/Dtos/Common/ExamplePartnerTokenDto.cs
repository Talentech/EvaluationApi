namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common
{
    /// <summary>
    /// This dto should contain everything needed to authenticate with the partner API on a customer's behalf, as this will be sent with every request from the EvaluationAPI to the API connector.
    /// Therefore it should uniquely identify the customer's subscription on the partner side, as well as contain the required authentication details towards the PartnerAPI
    /// How this is done is entirely up to the Partner to decide. It could be a reference to a token stored in the API connector, a username/password or JWT access token
    /// Therefore, the actual properties in this class just serves as an example
    /// </summary>
    public class ExamplePartnerTokenDto
    {
        public string CustomerAccountId { get; set; }
        public string PartnerApiRefreshToken { get; set; }
    }
}
