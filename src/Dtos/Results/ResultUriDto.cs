using System.Collections.Generic;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results
{
    public class ResultUriDto
    {
        public IEnumerable<string> EncryptedFields { get; set; }
        public string Name { get; set; }
        public UriType Type { get; set; }
        public string Uri { get; set; }
    }
}
