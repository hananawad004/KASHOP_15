using KASHOP.BLL;
using KASHOP.DAL.Data;
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
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetbyId(int id)
        //{
        //    var category = (await _categoryRepository.GetAsync()).FirstOrDefault(c => c.Id ==  id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(category);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Category category)
        //{
        //    await _categoryService.CreateAsync(category);
        //    return Ok(category);
        //}
    }

}
