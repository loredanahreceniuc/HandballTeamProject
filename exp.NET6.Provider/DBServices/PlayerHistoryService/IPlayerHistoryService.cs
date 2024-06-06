using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.PlayerHistoryService
{
    public interface IPlayerHistoryService
    {
        Task<Pagination<PlayerHistoryViewModel>> GetAllPlayerHistory(string? searchQuery, int pageNumber, int pageSize);
        Task<PlayerHistoryViewModel> GetPlayerHistory(int id);
        Task CreatePlayerHistory(CreatePlayerHistoryViewModel createPlayerHistory);
        Task UpdatePlayerHistory(int id, UpdatePlayerHistoryViewModel updatePlayerHistory);
        Task DeletePlayerHistoryPhysical(int id);
        Task DeletePlayerHistoryVirtual(int id);
    }
}
