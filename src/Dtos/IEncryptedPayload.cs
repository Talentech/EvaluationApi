using System.Collections.Generic;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos;

public interface IEncryptedPayload
{
    IEnumerable<string> EncryptedFields { get; set; }
}