using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.ArticleService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllArticleController(string? searchQuery,int pageNumber = 1, int pageSize = 10)
        {
            var article = await _articleService.GetAllArticle(searchQuery , pageNumber, pageSize);

            return Ok(article);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetArticleController(int id)
        {
            var article = await _articleService.GetArticle(id);

            return Ok(article);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateArticleController(CreateArticleViewModel createArticle)
        {
            await _articleService.CreateArticle(createArticle);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateArticleController(int id, UpdateArticleViewModel updateArticle)
        {
            await _articleService.UpdateArticle(id, updateArticle);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteArticlePhysicalController(int id)
        {
            await _articleService.DeleteArticlePhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteArticleVirtualController(int id)
        {
            await _articleService.DeleteArticleVirtual(id);

            return NoContent();
        }
    }
}



