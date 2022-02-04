using System.Collections.Generic;
using System.Linq;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common
{
    public class TriggeredByDto : IEncryptedPayload
    {
        public IEnumerable<string> EncryptedFields { get; set; } = Enumerable.Empty<string>();
        [EncryptedField] public string Email { get; set; }
        [EncryptedField] public string FirstName { get; set; }
        [EncryptedField] public string LastName { get; set; }
    }
}