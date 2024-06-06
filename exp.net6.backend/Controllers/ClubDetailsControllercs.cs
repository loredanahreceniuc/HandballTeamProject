using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.ClubDetailsService;

using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubDetailsController : ControllerBase
    {
        private readonly IClubDetailsService _clubDetailsService;
        public ClubDetailsController(IClubDetailsService clubDetailsService)
        {
            _clubDetailsService = clubDetailsService;
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetClubDetailsController()
        {
            var clubDetailsService = await _clubDetailsService.GetClubDetails();

            return Ok(clubDetailsService);
        }


        [HttpPut("edit")]
        public async Task<IActionResult> UpdateClubDetailsController(string? content)
        {
            await _clubDetailsService.UpdateClubDetails(content);

            return NoContent();
        }
    }
}