using NationalizeNameApi.Data;

namespace NationalizeNameApi.Models;

public class IndexViewModel {
	public string          Name            { get; set; }
	public CountryResponse CountryResponse { get; set; }
	public CountryFlagResponse[] CountryFlagResponse { get; set; }
	public Name          CountryCode     { get; set; }
}