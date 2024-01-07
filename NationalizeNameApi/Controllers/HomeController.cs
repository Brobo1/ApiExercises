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

/*	[HttpPost("/")]
	public async Task<IActionResult> Index(IndexViewModel model) {
		var country = await _natApi.GetCountryAsync(model.Name);
		var code    = await _countryFlagApi.GetCountryFlagAsync("ir");
		model.CountryResponse     = country;
		model.CountryFlagResponse = code;
		return View(model);
	}*/
	
	[HttpPost("/")]
	public async Task<IActionResult> Index(IndexViewModel model) {
		Console.WriteLine($"Searching for name: {model.Name}");
		var country = await _natApi.GetCountryAsync(model.Name);
    
		if(country != null && country.Country != null && country.Country.Any()) {
			Console.WriteLine($"Successfully retrieved data: {country.Country[0].CountryId}");
			var code = await _countryFlagApi.GetCountryFlagAsync(country.Country[0].CountryId);
			model.CountryResponse     = country;
			model.CountryFlagResponse = code;
			Console.WriteLine($"Successfully retrieved flag data: {model.CountryFlagResponse[0].Name.Common}");
		} else {
			Console.WriteLine("Failed to retrieve country data from Nationalize API.");
		}
		return View(model);
	}
}