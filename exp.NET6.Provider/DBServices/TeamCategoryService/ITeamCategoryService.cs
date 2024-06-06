using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.TeamCategoryService
{
    public interface ITeamCategoryService
    {
        Task<Pagination<TeamCategoryViewModel>> GetAllTeamCategories(string? searchQuery, int pageNumber, int pageSize);
        Task<TeamCategoryViewModel> GetTeamCategory(int id);
        Task CreateTeamCategory(CreateTeamCategoryViewModel createTeamCategory);
        Task UpdateTeamCategory(int id, UpdateTeamCategoryViewModel updateTeamCategory);
        Task DeleteTeamCategoryPhysical(int id);
        Task DeleteTeamCategoryVirtual(int id);
    }
}
