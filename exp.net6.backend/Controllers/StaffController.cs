using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.StaffService;
using Microsoft.AspNetCore.Mvc;

namespace exp.net6.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllStaffController(string? role, int pageNumber = 1, int pageSize = 10)
        {
            var staff = await _staffService.GetAllStaff(role, pageNumber, pageSize);

            return Ok(staff);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetStaffController(int id)
        {
            var staff = await _staffService.GetStaff(id);

            return Ok(staff);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStaffController(CreateStaffViewModel createStaff)
        {
            await _staffService.CreateStaff(createStaff);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateStaffController(int id, UpdateStaffViewModel updateStaff)
        {
            await _staffService.UpdateStaff(id, updateStaff);

            return NoContent();
        }

        [HttpPut("deletePhysical/{id}")]
        public async Task<IActionResult> DeleteStaffPhysicalController(int id)
        {
            await _staffService.DeleteStaffPhysical(id);

            return NoContent();
        }

        [HttpPut("deleteVirtual/{id}")]
        public async Task<IActionResult> DeleteStaffVirtualController(int id)
        {
            await _staffService.DeleteStaffVirtual(id);

            return NoContent();
        }
    }
}

