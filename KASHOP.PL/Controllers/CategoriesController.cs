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
       GenericRepository<Category> categoryRepository = new GenericRepository<Category>();

        [HttpGet("")]
        public IActionResult Index()
        {
            var categories = categoryRepository.Get();
            return Ok(categories);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetbyId(int id)
        //{
        //    var category = categoryRepository.Get().FirstOrDefault(c => c.Id ==  id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(category);
        //}

        [HttpPost]
        public IActionResult Create(Category category)
        {
            categoryRepository.Create(category);
            return Ok(category);
        }
    }
}
