using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using NationalizeNameApi.Data;
using NationalizeNameApi.Models;

namespace NationalizeNameApi.Controllers;

public class HomeController : Controller {
	private readonly INatApi         _natApi;
	private readonly ICountryFlagApi _countryFlagApi;

	public HomeController(INatApi natApi, ICountryFlagApi countryFlagApi) {
		_natApi         = natApi;
		_countryFlagApi = countryFlagApi;
	}

	[HttpGet("/")]
	public IActionResult Index() {
		return View(new IndexViewModel());
	}

	[HttpPost("/")]
	public async Task<IActionResult> Index(IndexViewModel model) {
		var country = await _natApi.GetCountryAsync(model.Name);
		model.CountryResponse = country;

		if (country != null && country.Country != null && country.Country.Any()) {
			var countryFlagResponses = new List<CountryFlagResponse>();

			foreach (var country1 in model.CountryResponse.Country) {
				var code = await _countryFlagApi.GetCountryFlagAsync(country1.CountryId);

				if (code != null && code.Length > 0) {
					countryFlagResponses.Add(code[0]);
				}
			}

			model.CountryFlagResponse = countryFlagResponses.ToArray();
		} else {
			Console.WriteLine("Failed to retrieve country data from Nationalize API.");
		}

		return View(model);
	}
}