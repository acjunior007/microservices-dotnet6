﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProductService _productService;

		public HomeController(ILogger<HomeController> logger, IProductService productService)
		{
			_logger = logger;
			_productService = productService;
		}

		public async Task<IActionResult> Index()
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var products = await _productService.FindAllProducts(token);
			return View(products);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Details(long id)
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var model = await _productService.FindProductById(id, token);

			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Logout()
		{
			return SignOut("Cookies", "oidc");
		}

		[Authorize]
		public async Task<IActionResult> Login()
		{
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			return RedirectToAction(nameof(Index));
		}
	}
}
