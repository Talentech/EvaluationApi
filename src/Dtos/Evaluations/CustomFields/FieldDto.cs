namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations.CustomFields
{
    [System.Text.Json.Serialization.JsonConverter(typeof(FieldConverter))]
    public class FieldDto
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public bool Disabled { get; set; }

        /// <summary>
        /// See FieldTypes class for valid names. 
        /// </summary>
        public string Type { get; set; }
    }
}
