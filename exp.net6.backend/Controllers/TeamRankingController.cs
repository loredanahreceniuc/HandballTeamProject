using exp.NET6.Infrastructure.Entities;
using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.TeamRankingService;

using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class TeamRankingController : ControllerBase
        {
            private readonly ITeamRankingService _teamRankingService;

            public TeamRankingController(ITeamRankingService teamRankingService)
            {
                _teamRankingService = teamRankingService;
            }

            [HttpGet("getAll")]
            public async Task<IActionResult> GetAllTeamRankingController(bool orderMatches, string? searchQuery, int pageNumber = 1, int pageSize = 10)
            {
                var teamRanking = await _teamRankingService.GetAllTeamRanking(searchQuery, pageNumber, pageSize, orderMatches);

                return Ok(teamRanking);
            }

            [HttpGet("get/{id}")]
            public async Task<IActionResult> GetTeamRankingController(int id)
            {
                var teamRanking = await _teamRankingService.GetTeamRanking(id);

                return Ok(teamRanking);
            }

            [HttpPost("create")]
            public async Task<IActionResult> CreateTeamRankingController(CreateTeamRankingViewModel createTeamRanking)
            {
                await _teamRankingService.CreateTeamRanking(createTeamRanking);

                return NoContent();
            }

            [HttpPut("edit/{id}")]
            public async Task<IActionResult> UpdateTeamRankingController(int id, UpdateTeamRankingViewModel updateTeamRanking)
            {
                await _teamRankingService.UpdateTeamRanking(id, updateTeamRanking);

                return NoContent();
            }

            [HttpPut("deletePhysical/{id}")]
            public async Task<IActionResult> DeleteTeamRankingPhysicalController(int id)
            {
                await _teamRankingService.DeleteTeamRankingPhysical(id);

                return NoContent();
            }

            [HttpPut("deleteVirtual/{id}")]
            public async Task<IActionResult> DeleteTeamRankingVirtualController(int id)
            {
                await _teamRankingService.DeleteTeamRankingVirtual(id);

                return NoContent();
            }
        }
    
}
