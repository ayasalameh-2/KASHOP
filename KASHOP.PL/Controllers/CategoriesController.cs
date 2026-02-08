using KASHOP.DAL.Data;
using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ApplicationDbContext context ,IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

    

       [HttpGet("")]
        public IActionResult index() {
            var categories = _context.categories.Include(c=>c.Translations).ToList();
            var response = categories.Adapt<List<CategoryResponse>>();

            return Ok(new { message = _localizer["success"].Value , response});
        }



        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _context.categories.Add(category);
            _context.SaveChanges();
            return Ok(new { message = _localizer["Success"].Value });
        }



    }


}

