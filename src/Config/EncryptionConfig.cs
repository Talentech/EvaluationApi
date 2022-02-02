namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Config
{
    public class EncryptionConfig
    {
        /// <summary>
        /// A unique identifier for this private key.
        /// Used by the source system to indicate what public key they used when encrypting the payload.
        /// </summary>
        public string KeyId { get; set; }

        /// <summary>
        /// A PKCS#1 RSA Public Key in Base64Url encoding.
        /// Please note that this is _not_ how you should handle your production keys.
        /// </summary>
        public string PrivateKey { get; set; }
    }
}