using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.TrophiesService;

using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrophiesController : ControllerBase
    {
        private readonly ITrophiesService _trophiesService;

        public TrophiesController(ITrophiesService trophiesService)
        {
            _trophiesService = trophiesService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTrophiesController(string? searchQuery,int pageNumber = 1, int pageSize = 5)
        {
            var trophies = await _trophiesService.GetAllTrophies(searchQuery, pageNumber, pageSize);

            return Ok(trophies);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTrophiesController(int id)
        {
            var trophies = await _trophiesService.GetTrophies(id);

            return Ok(trophies);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTrophiesController(CreateTrophiesViewModel createTrophies)
        {
            await _trophiesService.CreateTrophies(createTrophies);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateTrophiesController(int id, UpdateTrophiesViewModel updateTrophies)
        {
            await _trophiesService.UpdateTrophies(id, updateTrophies);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteTrophiesPhysicalController(int id)
        {
            await _trophiesService.DeleteTrophiesPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteTrophiesVirtualController(int id)
        {
            await _trophiesService.DeleteTrophiesVirtual(id);

            return NoContent();
        }
    }
}
