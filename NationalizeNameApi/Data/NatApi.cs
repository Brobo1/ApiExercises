using System.Text.Json;
using System.Text.Json.Serialization;

namespace NationalizeNameApi.Data;

public interface INatApi {
	Task<CountryResponse> GetCountryAsync(string name);
}

public class NatApi : INatApi {
	private readonly HttpClient _httpClient;

	public NatApi(HttpClient httpClient) {
		_httpClient = httpClient;
	}

	public async Task<CountryResponse> GetCountryAsync(string name) {
		var response = await _httpClient.GetAsync($"https://api.nationalize.io/?name={name}");
		return await response.Content.ReadFromJsonAsync<CountryResponse>();
	}
}

public class CountryResponse {
	[JsonPropertyName("count")]
	public int Count { get; set; }
	public List<Country> Country { get; set; } = new();
}

public class Country {
	
	[JsonPropertyName("country_id")]
	public string CountryId { get; set; }

	[JsonPropertyName("probability")]
	public double Probability { get; set; }
}