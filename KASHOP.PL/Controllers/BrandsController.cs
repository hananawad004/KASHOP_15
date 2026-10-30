using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        //ApplicationDbContext context = new ApplicationDbContext();

        //[HttpGet("")]
        //public IActionResult Index()
        //{
        //    var brands = context.Brands.ToList();
        //    return Ok(brands);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetbyId(int id)
        //{
        //    var brand = context.Brands.Find(id);
        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(brand);
        //}

        //[HttpPost]
        //public IActionResult Create(Brand brand)
        //{
        //    context.Brands.Add(brand);
        //    context.SaveChanges();
        //    return Ok(brand);
        //}
    }
}

