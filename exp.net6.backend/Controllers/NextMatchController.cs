using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.NextMatchService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NextMatchController : ControllerBase
    {
        private readonly INextMatchService _nextMatchService;

        public NextMatchController(INextMatchService nextMatchService)
        {
            _nextMatchService = nextMatchService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllNextMatchController(string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var nextMatches = await _nextMatchService.GetAllNextMatch(searchQuery, pageNumber, pageSize);

            return Ok(nextMatches);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetNextMatchController(int id)
        {
            var nextMatches = await _nextMatchService.GetNextMatch(id);

            return Ok(nextMatches);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNextMatchController(CreateNextMatchViewModel createNextMatch)
        {
            await _nextMatchService.CreateNextMatch(createNextMatch);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateNextMatchController(int id, UpdateNextMatchViewModel updateNextMatch)
        {
            await _nextMatchService.UpdateNextMatch(id, updateNextMatch);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteNextMatchPhysicalController(int id)
        {
            await _nextMatchService.DeleteNextMatchPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteNextMatchVirtualController(int id)
        {
            await _nextMatchService.DeleteNextMatchVirtual(id);

            return NoContent();
        }
    }
}
