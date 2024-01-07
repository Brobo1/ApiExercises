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
		var code    = await _countryFlagApi.GetCountryFlagAsync(country.Country[0].CountryId);
		model.CountryResponse     = country;
		model.CountryFlagResponse = code;
		return View(model);
	}

}