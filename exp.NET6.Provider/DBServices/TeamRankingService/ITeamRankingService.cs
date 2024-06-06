using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.TeamRankingService
{
    public interface ITeamRankingService
    {
        Task<Pagination<TeamRankingViewModel>> GetAllTeamRanking(string? searchQuery, int pageNumber, int pageSize,  bool orderMatches);
        Task<TeamRankingViewModel> GetTeamRanking(int id);
        Task CreateTeamRanking(CreateTeamRankingViewModel createTeamRanking);
        Task UpdateTeamRanking(int id, UpdateTeamRankingViewModel updateTeamRanking);
        Task DeleteTeamRankingPhysical(int id);
        Task DeleteTeamRankingVirtual(int id);
    }
}
