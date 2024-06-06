using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.ArticleService;
using exp.NET6.Services.DBServices.MatchService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllMatchController(string? searchQuery, DateTime? date, int? competitionId, int pageNumber = 1, int pageSize = 10)
        {
            var matches = await _matchService.GetAllMatch(searchQuery,date,competitionId, pageNumber, pageSize);

            return Ok(matches);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetMatchController(int id)
        {
            var matches = await _matchService.GetMatch(id);

            return Ok(matches);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMatchController(CreateMatchViewModel createMatch)
        {
            await _matchService.CreateMatch(createMatch);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateMatchController(int id, UpdateMatchViewModel updateMatch)
        {
            await _matchService.UpdateMatch(id, updateMatch);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteMatchPhysicalController(int id)
        {
            await _matchService.DeleteMatchPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteMatchVirtualController(int id)
        {
            await _matchService.DeleteMatchVirtual(id);

            return NoContent();
        }
    }
}
