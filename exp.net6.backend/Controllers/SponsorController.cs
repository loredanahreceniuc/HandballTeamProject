using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.SponsorService;

using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _sponsorService;

        public SponsorController(ISponsorService sponsorService)
        {
            _sponsorService = sponsorService ?? throw new ArgumentNullException(nameof(sponsorService));
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllSponsors(string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var sponsors = await _sponsorService.GetAllSponsors(searchQuery, pageNumber, pageSize);

            return Ok(sponsors);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSponsor(int id)
        {
            var sponsors = await _sponsorService.GetSponsor(id);

            return Ok(sponsors);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSponsor(CreateSponsorViewModel createSponsor)
        {
            await _sponsorService.CreateSponsor(createSponsor);

            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSponsor(int id, UpdateSponsorViewModel updateSponsor)
        {
            await _sponsorService.UpdateSponsor(id, updateSponsor);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSponsor(int id)
        {
            await _sponsorService.DeleteSponsor(id);

            return NoContent();
        }
    }
}
