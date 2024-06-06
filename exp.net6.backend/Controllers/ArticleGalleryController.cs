using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.ArticleGallerys;

using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleGalleryController : ControllerBase
    {
        private readonly IArticleGalleryService _articleGalleryService;

        public ArticleGalleryController(IArticleGalleryService articleGalleryService)
        {
            _articleGalleryService = articleGalleryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllArtcileGalleryController(int pageNumber = 1, int pageSize = 10)
        {
            var articleGallery = await _articleGalleryService.GetAllArticleGallery(pageNumber, pageSize);

            return Ok(articleGallery);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetArticleGalleryController(int id)
        {
            var articleGallery = await _articleGalleryService.GetArticleGallery(id);
            
            return Ok(articleGallery);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateArticleGalleryController(CreateArticleGalleryViewModel createArticleGallery)
        {
            await _articleGalleryService.CreateArticleGallery(createArticleGallery);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateArticleGalleryController(int id, UpdateArticleGalleryViewModel updateArticleGallery)
        {
            await _articleGalleryService.UpdateArticleGallery(id, updateArticleGallery);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteArticleGalleryPhysicalController(int id)
        {
            await _articleGalleryService.DeleteArticleGalleryPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteArticleGalleryVirtualController(int id)
        {
            await _articleGalleryService.DeleteArticleGalleryVirtual(id);

            return NoContent();
        }
    }

}
