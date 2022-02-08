using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Encryption
{
    internal class SourceSystemEncryption
    {
        public string KeyId { get; set; }
        public RSA Encryptor { get; set; }
    }

    public class EncryptionService : IEncryptionService
    {
        private readonly EncryptionConfig _config;
        private readonly RSA _decryptor;

        private readonly object _dictLock = new object();
        private readonly Dictionary<string, SourceSystemEncryption> _sourceSystemPublicKeys;

        public EncryptionService(EncryptionConfig config)
        {
            _config = config;
            _sourceSystemPublicKeys = new Dictionary<string, SourceSystemEncryption>();

            if (string.IsNullOrEmpty(config.PrivateKey)) return;
            _decryptor = RSA.Create();
            _decryptor.ImportRSAPrivateKey(WebEncoders.Base64UrlDecode(_config.PrivateKey), out _);
        }

        public void Decrypt<T>(T obj) where T : IEncryptedPayload
        {
            var props = obj.GetType().GetProperties();

            foreach (var field in obj.EncryptedFields)
            {
                var piEncryptedField =
                    props.FirstOrDefault(p => field.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
                if (piEncryptedField == null || piEncryptedField.GetValue(obj) is not string propertyValue ||
                    string.IsNullOrEmpty(propertyValue)) continue;

                piEncryptedField.SetValue(obj, DecryptField(propertyValue));
            }
        }

        public string DecryptField(string encryptedField)
        {
            if (_decryptor == null)
                throw new InvalidOperationException("EncryptionService has not been configured");

            var parts = encryptedField.Split(':');

            var keyId = parts[0];
            if (keyId != _config.KeyId)
                throw new InvalidOperationException($"Unable to decrypt EncryptedField with unknown keyId {keyId}");

            var encodedValue = parts[1];
            var encryptedValue = WebEncoders.Base64UrlDecode(encodedValue);
            var decryptedValue = _decryptor.Decrypt(encryptedValue, RSAEncryptionPadding.OaepSHA256);
            var plaintextValue = Encoding.UTF8.GetString(decryptedValue);

            return plaintextValue;
        }

        public void Encrypt<T>(string sourceSystem, T obj) where T : IEncryptedPayload
        {
            var props = obj.GetType().GetProperties()
                .Where(p => p.GetCustomAttributes<EncryptedFieldAttribute>().Any());

            var encryptedFields = new List<string>();
            foreach (var piEncryptedField in props)
            {
                if (piEncryptedField.GetValue(obj) is not string propertyValue ||
                    string.IsNullOrEmpty(propertyValue)) continue;

                piEncryptedField.SetValue(obj, EncryptField(sourceSystem, propertyValue));
                encryptedFields.Add(piEncryptedField.Name);
            }

            obj.EncryptedFields = encryptedFields;
        }

        public string EncryptField(string sourceSystem, string value)
        {
            var byteValue = Encoding.UTF8.GetBytes(value);

            SourceSystemEncryption sourceSystemEncryption;
            lock (_dictLock)
            {
                if (!_sourceSystemPublicKeys.TryGetValue(sourceSystem, out sourceSystemEncryption))
                    throw new InvalidOperationException($"No public key for SourceSystem {sourceSystem} in store");
            }

            var encryptedValue = sourceSystemEncryption.Encryptor.Encrypt(byteValue, RSAEncryptionPadding.OaepSHA256);
            var encodedValue = WebEncoders.Base64UrlEncode(encryptedValue);

            var encryptedField = $"{sourceSystemEncryption.KeyId}:{encodedValue}";

            return encryptedField;
        }

        public void StoreSourceSystemPublicKey(string sourceSystem, string sourceSystemPublicKeyField)
        {
            lock (_dictLock)
            {
                var parts = sourceSystemPublicKeyField.Split(':');
                var keyId = parts[0];
                if (_sourceSystemPublicKeys.ContainsKey(sourceSystem) &&
                    _sourceSystemPublicKeys[sourceSystem].KeyId == keyId)
                    return;

                var encodedPublicKey = parts[1];
                var publicKey = WebEncoders.Base64UrlDecode(encodedPublicKey);

                var encryptor = RSA.Create();
                encryptor.ImportRSAPublicKey(publicKey, out _);

                _sourceSystemPublicKeys[sourceSystem] = new SourceSystemEncryption
                {
                    KeyId = keyId,
                    Encryptor = encryptor
                };
            }
        }

        public bool ShouldEncryptForSourceSystem(string sourceSystem)
        {
            lock (_dictLock)
            {
                return _sourceSystemPublicKeys.ContainsKey(sourceSystem);
            }
        }
    }
}