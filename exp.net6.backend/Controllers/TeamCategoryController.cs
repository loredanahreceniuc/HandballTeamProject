using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.TeamCategoryService;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamCategoryController : ControllerBase
    {
        private readonly ITeamCategoryService _teamCategoryService;

        public TeamCategoryController(ITeamCategoryService teamCategoryService)
        {
            _teamCategoryService = teamCategoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTeamCategoriesController(string? searchQuery,int pageNumber = 1, int pageSize = 10)
        {
            var teamCategories = await _teamCategoryService.GetAllTeamCategories(searchQuery, pageNumber , pageSize);

            return Ok(teamCategories);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetTeamCategoryController(int id)
        {
            var teamCategory = await _teamCategoryService.GetTeamCategory(id);

            return Ok(teamCategory);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTeamCategoryController(CreateTeamCategoryViewModel createTeamCategory)
        {
            await _teamCategoryService.CreateTeamCategory(createTeamCategory);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateTeamCategoryController(int id, UpdateTeamCategoryViewModel updateTeamCategory)
        {
            await _teamCategoryService.UpdateTeamCategory(id, updateTeamCategory);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteTeamCategoryPhysicalController(int id)
        {
            await _teamCategoryService.DeleteTeamCategoryPhysical(id);

            return NoContent();
        }
        
        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteTeamCategoryVirtualController(int id)
        {
            await _teamCategoryService.DeleteTeamCategoryVirtual(id);

            return NoContent();
        }
    }
}
