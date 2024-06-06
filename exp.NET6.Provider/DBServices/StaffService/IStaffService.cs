using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.StaffService
{
    public interface IStaffService
    {
        Task<Pagination<StaffViewModel>> GetAllStaff(string? role, int pageNumber, int pageSize);
        Task<StaffViewModel> GetStaff(int id);
        Task CreateStaff(CreateStaffViewModel createStaff);
        Task UpdateStaff(int id, UpdateStaffViewModel updateStaff);
        Task DeleteStaffPhysical(int id);
        Task DeleteStaffVirtual(int id);
    }
}
