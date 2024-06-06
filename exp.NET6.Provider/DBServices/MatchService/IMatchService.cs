using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.MatchService
{
    public interface IMatchService
    {
        Task<Pagination<MatchViewModel>> GetAllMatch(string? searchQuery, DateTime? date, int? competitionId, int pageNumber, int pageSize);
        Task<MatchViewModel> GetMatch(int id);
        Task CreateMatch(CreateMatchViewModel createMatch);
        Task UpdateMatch(int id,UpdateMatchViewModel updateMatch);
        Task DeleteMatchPhysical(int id);
        Task DeleteMatchVirtual(int id);
    }
}
