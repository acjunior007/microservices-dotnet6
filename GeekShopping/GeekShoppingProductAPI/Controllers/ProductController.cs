using GeekShoppingProductAPI.Data.ValueObjects;
using GeekShoppingProductAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoppingProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            try
            {
                var products = await _productRepository.FindAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            try
            {
                var product = await _productRepository.FindById(id);
                if (product == null) return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Post([FromBody] ProductVO vo)
        {
            try
            {
                if (vo == null) return BadRequest();

                var product = await _productRepository.Create(vo);
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductVO>> Put(long id, [FromBody] ProductVO vo)
        {
            try
            {
                if (vo == null || vo.Id != id) return BadRequest();
                var product = await _productRepository.Update(vo);
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                var resp = await _productRepository.DeleteById(id);
                return resp ? Ok(resp) : BadRequest(resp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
