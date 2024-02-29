using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
	public class ProductController : Controller
	{
		private IProductService _productService;

		string token = string.Empty;
		public ProductController(IProductService productService)
		{
			_productService = productService;
			// token = HttpContext.GetTokenAsync("access_token").Result;
		}

		[Authorize]
		[HttpGet(Name = "ListProduct")]
		public async Task<IActionResult> ProductIndex()
		{
			token = await HttpContext.GetTokenAsync("access_token");
			var products = await _productService.FindAllProducts(token);
			return View(products);
		}

		public async Task<IActionResult> ProductCreate()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> ProductCreate(ProductModel model)
		{
			if (ModelState.IsValid)
			{
				token = await HttpContext.GetTokenAsync("access_token");
				var response = await _productService.CreateProduct(model, token);
				if (response != null) return RedirectToAction("ProductIndex");
			}

			return View(model);
		}

		public async Task<IActionResult> ProductUpdate(long id)
		{
			token = await HttpContext.GetTokenAsync("access_token");
			var product = await _productService.FindProductById(id, token);
			if (product != null) return View(product);
			return NotFound();
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> ProductUpdate(ProductModel model)
		{
			if (ModelState.IsValid)
			{
				token = await HttpContext.GetTokenAsync("access_token");
				var response = await _productService.UpdateProduct(model, token);
				if (response != null) return RedirectToAction("ProductIndex");
			}

			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> ProductDelete(long id)
		{
			token = await HttpContext.GetTokenAsync("access_token");
			var product = await _productService.FindProductById(id, token);
			if (product != null) return View(product);
			return NotFound();
		}

		[Authorize(Roles = Role.Admin)]
		[HttpPost]
		public async Task<IActionResult> ProductDelete(ProductModel model)
		{
			token = await HttpContext.GetTokenAsync("access_token");
			var response = await _productService.DeleteProduct(model.Id, token);
			if (response) return RedirectToAction(nameof(ProductIndex));

			return View(model);
		}
	}

}
