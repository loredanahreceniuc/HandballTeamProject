using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.TrophiesService
{
    public interface ITrophiesService
    {
        Task<Pagination<TrophiesViewModel>> GetAllTrophies(string? searchQuery, int pageNumber, int pageSize);
        Task<TrophyViewModel> GetTrophies(int id);
        Task CreateTrophies(CreateTrophiesViewModel createTrophies);
        Task UpdateTrophies(int id, UpdateTrophiesViewModel updateTrophies);
        Task DeleteTrophiesPhysical(int id);
        Task DeleteTrophiesVirtual(int id);
    }
}
