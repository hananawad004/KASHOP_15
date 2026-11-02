using KASHOP.BLL;
using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryService.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest category, CancellationToken cancellationToken = default)
        {
            var result = await _categoryService.CreateAsync(category, cancellationToken);
            if (!result) return BadRequest();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _categoryService.DeleteAsync(id, cancellationToken);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryRequest category, CancellationToken cancellationToken = default)
        {
            var result = await _categoryService.UpdateAsync(id, category, cancellationToken);
            if (!result) return NotFound();
            return Ok();
        }

    }

}
