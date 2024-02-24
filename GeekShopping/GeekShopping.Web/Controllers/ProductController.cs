using GeekShopping.Web.Models;
using GeekShopping.Web.Services;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "ListProduct")]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if (response != null) return RedirectToAction("ProductIndex");
            }

            return View(model);
        }

		public async Task<IActionResult> ProductUpdate(long id)
		{
            var product = await _productService.FindProductById(id);
            if (product != null) return View(product);
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ProductUpdate(ProductModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _productService.UpdateProduct(model);
				if (response != null) return RedirectToAction("ProductIndex");
			}

			return View(model);
		}
	}

}
