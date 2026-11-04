using KASHOP.BLL;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetAllAsync(cancellationToken);
            return Ok(products);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]ProductRequest request,CancellationToken cancellationToken=default)
        {
            var result = await _productService.CreateAsync(request, cancellationToken);
            if (!result) return BadRequest("Invalid product data");
            return Ok();
        }

     
    }
}
