using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.PlayerService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPlayerController(bool? mainTeamValue, string? position, int pageSize = 10, int pageNumber = 1)
        {
            var player = await _playerService.GetAllPlayer(mainTeamValue, position, pageSize, pageNumber);

            return Ok(player);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPlayerController(int id)
        {
            var player = await _playerService.GetPlayer(id);

            return Ok(player);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayerController(CreatePlayerViewModel createPlayer)
        {
            await _playerService.CreatePlayer(createPlayer);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdatePlayerController(int id, UpdatePlayerViewModel updatePlayer)
        {
            await _playerService.UpdatePlayer(id, updatePlayer);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeletePlayerPhysicalController(int id)
        {
            await _playerService.DeletePlayerPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeletePlayerVirtualController(int id)
        {
            await _playerService.DeletePlayerVirtual(id);

            return NoContent();
        }
    }
}


