using System.Collections.Generic;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results
{
    public class ResultUriDto : IEncryptedPayload
    {
        /// <summary>
        /// Empty, or ["Uri"] when Uri is encrypted. Null or any other value is rejected with 400.
        /// </summary>
        public IEnumerable<string> EncryptedFields { get; set; } = [];
        public string Name { get; set; }
        public UriType Type { get; set; }
        [EncryptedField] public string Uri { get; set; }
    }
}