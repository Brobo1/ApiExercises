using System.Text.Json.Serialization;

namespace NationalizeNameApi.Data;

public interface ICountryFlagApi {
	Task<CountryFlagResponse[]> GetCountryFlagAsync(string countryCode);
}

public class CountryFlagApi : ICountryFlagApi {
	private readonly HttpClient _httpClient;

	public CountryFlagApi(HttpClient httpClient) {
		_httpClient = httpClient;
	}

	public async Task<CountryFlagResponse[]> GetCountryFlagAsync(string countryCode) {
		var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/alpha/{countryCode}");
		return await response.Content.ReadFromJsonAsync<CountryFlagResponse[]>();
	}
}

public class CountryFlagResponse
{
    [JsonPropertyName("name")]
    public Name Name { get; set; } 
}

public class Name
{
    [JsonPropertyName("common")]
    public string Common { get; set; }
}