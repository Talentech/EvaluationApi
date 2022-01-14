using System.Collections.Generic;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common
{
    public class TriggeredByDto
    {
        public IEnumerable<string> EncryptedFields { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
