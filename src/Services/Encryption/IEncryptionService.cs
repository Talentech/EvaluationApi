using System.Security.Cryptography;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Encryption
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Attempts to decrypt an object, e.g. a DTO.
        /// </summary>
        void Decrypt<T>(T obj) where T : IEncryptedPayload;

        /// <summary>
        /// Decrypts an encrypted field given that the service is configured with a RSA private key
        /// </summary>
        /// <param name="encryptedField">An encrypted field in the format {keyId}:{base64url encoded encrypted value}</param>
        /// <returns>Decrypted, plaintext value</returns>
        string DecryptField(string encryptedField);

        /// <summary>
        /// Attempts to encrypt an object using the persisted key for the source system
        /// </summary>
        /// <param name="sourceSystem">The source system as provided in e.g. the invitation payload</param>
        /// <param name="obj">e.g. the DTO</param>
        void Encrypt<T>(string sourceSystem, T obj) where T : IEncryptedPayload;

        /// <summary>
        /// Encrypts a value using the RSA public key stored for the sourceSystem (ATS).
        /// </summary>
        /// <param name="sourceSystem">The source system as provided in e.g. the invitation payload</param>
        /// <param name="value">Plaintext value to encrypt</param>
        /// <returns>Encrypted field value in the format {keyId}:{base64url encoded encrypted value}</returns>
        string EncryptField(string sourceSystem, string value);

        /// <summary>
        /// Stores a source system's public key so that it can be retrieved when posting results
        /// </summary>
        /// <param name="sourceSystem">The source system as provided in e.g. the invitation payload</param>
        /// <param name="sourceSystemPublicKeyField">A public key field in the format {keyId}:{base64url encoded RSA PKCS#1 public key}</param>
        void StoreSourceSystemPublicKey(string sourceSystem, string sourceSystemPublicKeyField);

        /// <summary>
        /// Used to check whether or not a source system supports encryption
        /// </summary>
        /// <param name="sourceSystem">The source system as provided in e.g. the invitation payload</param>
        /// <returns></returns>
        bool ShouldEncryptForSourceSystem(string sourceSystem);
    }
}