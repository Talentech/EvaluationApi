namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations;

public record PartnerAddressDto
{
	/// <summary>
	/// The c/o part of the address. Optional to use by the partner.
	/// </summary>
	/// <remarks>
	/// * Can be encrypted
	/// * In case it is encrypted, value "Address.CareOf" should be presented in the EncryptedFields list.
	/// </remarks>
	[EncryptedField]
	public string? CareOf { get; init; }
	
	/// <summary>
	/// The address (street, house, etc.) part of the address. Optional to use by the partner.
	/// </summary>
	/// <remarks>
	/// * Can be encrypted
	/// * In case it is encrypted, value "Address.Address" should be presented in the EncryptedFields list.
	/// </remarks>
	[EncryptedField]
	public string? Address { get; init; }
	
	/// <summary>
	/// The postal code part of the address. Optional to use by the partner.
	/// </summary>
	/// <remarks>
	/// * Can be encrypted
	/// * In case it is encrypted, value "Address.PostalCode" should be presented in the EncryptedFields list.
	/// </remarks>	
	[EncryptedField]
	public string? PostalCode { get; init; }

	/// <summary>
	/// The city part of the address. Optional to use by the partner.
	/// </summary>
	/// <remarks>
	/// * Can be encrypted
	/// * In case it is encrypted, value "Address.PostalCity" should be presented in the EncryptedFields list.
	/// </remarks>	
	[EncryptedField]
	public string? PostalCity { get; init; }
	
	/// <summary>
	/// The municipality part of the address. Optional to use by the partner.
	/// </summary>
	/// <remarks>
	/// * Can be encrypted
	/// * In case it is encrypted, value "Address.Municipality" should be presented in the EncryptedFields list.
	/// </remarks>	
	[EncryptedField]
	public string? Municipality { get; init; }
	
	/// <summary>
	/// The country part of the address. Optional to use by the partner.
	/// </summary>
	/// <remarks>
	/// * Can be encrypted
	/// * In case it is encrypted, value "Address.Country" should be presented in the EncryptedFields list.
	/// </remarks>	
	[EncryptedField]
	public string? Country { get; init; }
}