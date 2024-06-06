using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.CompetitionService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCompetitionController(string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var competition = await _competitionService.GetAllCompetition(searchQuery, pageNumber, pageSize);

            return Ok(competition);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCompetitionController(int id)
        {
            var competition = await _competitionService.GetCompetition(id);

            return Ok(competition);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCompetitionController(CreateCompetitionViewModel createCompetition)
        {
            await _competitionService.CreateCompetition(createCompetition);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateCompetitionController(int id, UpdateCompetitionViewModel updateCompetition)
        {
            await _competitionService.UpdateCompetition(id, updateCompetition);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteCompetitionPhysicalController(int id)
        {
            await _competitionService.DeleteCompetitionPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteCompetitionVirtualController(int id)
        {
            await _competitionService.DeleteCompetitionVirtual(id);

            return NoContent();
        }
    }
}
