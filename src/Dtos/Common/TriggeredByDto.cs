using System.Collections.Generic;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common
{
    public class TriggeredByDto : IEncryptedPayload
    {
        public IEnumerable<string> EncryptedFields { get; set; } = [];
        [EncryptedField] public string Email { get; set; } = null!;
        [EncryptedField] public string FirstName { get; set; } = null!;
        [EncryptedField] public string LastName { get; set; } = null!;

        /// <summary>
        /// Sent on invitations, when the ATS has it.
        /// </summary>
        [EncryptedField] public string? ExternalUserIdentifier { get; set; }
    }
}