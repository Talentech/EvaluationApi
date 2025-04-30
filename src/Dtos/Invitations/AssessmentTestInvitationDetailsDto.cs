using System;
using System.Collections.Generic;
using System.Linq;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations
{
    public class AssessmentTestInvitationDetailsDto : IEncryptedPayload
    {
        /// <summary>
        /// Unique id of the invitation. Must be included when posting results back to the EvaluationApi.
        /// </summary>
        public Guid InvitationId { get; set; }

        /// <summary>
        /// An id of the ATS sending the request, i.e. "talentrecruiter", "reachmee" or similar. Partner can use this for monitoring purposes.
        /// </summary>
        public string SourceSystem { get; set; }

        /// <summary>
        /// A public key in the format keyId:publicKey. If supplied, the ATS supports encrypted fields.
        /// </summary>
        public string SourceSystemPublicKey { get; set; }

        /// <summary>
        /// The ATS project id. Optional to use by the partner. Not necessarily unique across multiple customers.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// The ATS project name. Optional to use by the partner.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// The id of the evaluation. Will correspond to one of the available evaluations listed by the partner.
        /// </summary>
        public string EvaluationFormId { get; set; }

        /// <summary>
        /// The candidate personal ID. Optional to use by the partner.
        /// </summary>
        /// <remarks>
        /// * Can be encrypted
        /// * In case it is encrypted, value "PersonalId" should be presented in the EncryptedFields list.
        /// </remarks>
        [EncryptedField]
        public string? PersonalId { get; init; }
        
        /// <summary>
        /// First name of the candidate
        /// </summary>
        [EncryptedField]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the candidate
        /// </summary>
        [EncryptedField]
        public string LastName { get; set; }

        /// <summary>
        /// The email address of the candidate
        /// </summary>
        [EncryptedField]
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the candidate. Included if available and applicable in ATSes
        /// </summary>
        [EncryptedField]
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// The address of the candidate. Optional to use by the partner.
        /// </summary>        
        public PartnerAddressDto? Address { get; init; }        

        /// <summary>
        /// The language code from the list of languages given by the partner for each evaluation. Partner can ignore this if not applicable.
        /// </summary>
        public string PreferredLanguage { get; set; }

        /// <summary>
        /// Can be included in email or in a greeting to the candidate. Optionally sent from ATSes, so a default text should be inserted by partner (if applicable)
        /// </summary>
        public string NoteToCandidate { get; set; }

        /// <summary>
        /// List of field values referencing the fields in the CustomFields property of the Evaluation form.
        /// Only applicable if custom fields are included for the forms returned from the /evaluationforms endpoint.
        /// </summary>
        public List<FieldValueDto> CustomFieldValues { get; set; }

        public IEnumerable<string> EncryptedFields { get; set; } = Enumerable.Empty<string>();
    }
}