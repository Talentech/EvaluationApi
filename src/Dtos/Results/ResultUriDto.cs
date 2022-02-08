using System.Collections.Generic;
using System.Linq;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results
{
    public class ResultUriDto : IEncryptedPayload
    {
        public IEnumerable<string> EncryptedFields { get; set; } = Enumerable.Empty<string>();
        public string Name { get; set; }
        public UriType Type { get; set; }
        [EncryptedField] public string Uri { get; set; }
    }
}