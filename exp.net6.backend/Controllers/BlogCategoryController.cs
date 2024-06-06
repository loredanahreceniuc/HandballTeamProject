
using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.BlogCategories;

using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService _blogCategoryService;
        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBlogCategoryController(string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var blogCategoryService = await _blogCategoryService.GetAllBlogCategory(searchQuery ,pageNumber, pageSize);

            return Ok(blogCategoryService);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetBlogCategoryController(int id)
        {
            var blogCategoryService = await _blogCategoryService.GetBlogCategory(id);

            return Ok(blogCategoryService);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBlogCategoryController(CreateBlogCategoryViewModel createBlogCategory)
        {
            await _blogCategoryService.CreateBlogCategory(createBlogCategory);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateBlogCategoryController(int id, UpdateBlogCategoryViewModel updateBlogCategory)
        {
            await _blogCategoryService.UpdateBlogCategory(id, updateBlogCategory);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteBlogCategoryPhysicalController(int id)
        {
            await _blogCategoryService.DeleteBlogCategoryPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteBlogCategoryVirtualController(int id)
        {
            await _blogCategoryService.DeleteBlogCategoryVirtual(id);

            return NoContent();
        }
    }
}
