using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.PlayerGalleryService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class PlayerGalleryController : ControllerBase
        {
            private readonly IPlayerGalleryService _playerGalleryService;

            public PlayerGalleryController(IPlayerGalleryService playerGalleryService)
            {
                _playerGalleryService = playerGalleryService;
            }

            [HttpGet("getAll")]
            public async Task<IActionResult> GetAllPlayerGalleryController(int pageNumber = 1, int pageSize = 10)
            {
                var playerGallery = await _playerGalleryService.GetAllPlayerGallery(pageNumber, pageSize);

                return Ok(playerGallery);
            }

            [HttpGet("get/{id}")]
            public async Task<IActionResult> GetPlayerGalleryController(int id)
            {
                var playerGallery = await _playerGalleryService.GetPlayerGallery(id);

                return Ok(playerGallery);
            }

            [HttpPost("create")]
            public async Task<IActionResult> CreatePlayerGalleryController(CreatePlayerGalleryViewModel createPlayerGallery)
            {
                await _playerGalleryService.CreatePlayerGallery(createPlayerGallery);

                return NoContent();
            }

            [HttpPut("edit/{id}")]
            public async Task<IActionResult> UpdatePlayerGalleryController(int id, UpdatePlayerGalleryViewModel updatePlayerGallery)
            {
                await _playerGalleryService.UpdatePlayerGallery(id, updatePlayerGallery);

                return NoContent();
            }

            [HttpPut("deletePhysical/{id}")]
            public async Task<IActionResult> DeletePlayerGalleryPhysicalController(int id)
            {
                await _playerGalleryService.DeletePlayerGalleryPhysical(id);

                return NoContent();
            }

            [HttpPut("deleteVirtual/{id}")]
            public async Task<IActionResult> DeletePlayerGalleryVirtualController(int id)
            {
                await _playerGalleryService.DeletePlayerGalleryVirtual(id);

                return NoContent();
        }
        }

    }

