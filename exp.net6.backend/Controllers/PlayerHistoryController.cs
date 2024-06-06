using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.PlayerHistoryService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlayerHistoryController : ControllerBase
    {
        private readonly IPlayerHistoryService _playerhistoryService;

        public PlayerHistoryController(IPlayerHistoryService playerhistoryService)
        {
            _playerhistoryService = playerhistoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPlayerHistoryController(string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var playerhistory = await _playerhistoryService.GetAllPlayerHistory(searchQuery, pageNumber, pageSize);

            return Ok(playerhistory);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPlayerHistoryController(int id)
        {
            var playerhistory = await _playerhistoryService.GetPlayerHistory(id);

            return Ok(playerhistory);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayerHistoryController(CreatePlayerHistoryViewModel createPlayerHistory)
        {
            await _playerhistoryService.CreatePlayerHistory(createPlayerHistory);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdatePlayerHistoryController(int id, UpdatePlayerHistoryViewModel updatePlayerHistory)
        {
            await _playerhistoryService.UpdatePlayerHistory(id, updatePlayerHistory);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeletePlayerHistoryPhysicalController(int id)
        {
            await _playerhistoryService.DeletePlayerHistoryPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeletePlayerVirtualController(int id)
        {
            await _playerhistoryService.DeletePlayerHistoryVirtual(id);

            return NoContent();
        }
    }
}
