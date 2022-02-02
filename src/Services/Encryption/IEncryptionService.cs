using System.Security.Cryptography;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Encryption
{
    public interface IEncryptionService
    {
        string DecryptField(string encryptedField);
        string EncryptField(string sourceSystem, string value);
        void StoreSourceSystemPublicKey(string sourceSystem, string sourceSystemPublicKeyField);
        bool ShouldEncryptForSourceSystem(string sourceSystem);
    }
}