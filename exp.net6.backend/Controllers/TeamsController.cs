using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.TeamService;

using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTeamsController(string? searchQuery,int pageNumber = 1, int pageSize = 5)
        {
            var teams = await _teamsService.GetAllTeams(searchQuery, pageNumber, pageSize);

            return Ok(teams);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTeamsController(int id)
        {
            var teams = await _teamsService.GetTeam(id);

            return Ok(teams);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTeamsController(CreateTeamsViewModel createTeams)
        {
            await _teamsService.CreateTeams(createTeams);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateTeamsController(int id, UpdateTeamsViewModel updateTeams)
        {
            await _teamsService.UpdateTeams(id, updateTeams);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteTeamsPhysicalController(int id)
        {
            await _teamsService.DeleteTeamsPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteTeamsVirtualController(int id)
        {
            await _teamsService.DeleteTeamsVirtual(id);

            return NoContent();
        }
    }
}
