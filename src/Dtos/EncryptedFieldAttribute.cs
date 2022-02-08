using System;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class EncryptedFieldAttribute : Attribute
    {
    }
}