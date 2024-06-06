using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.TeamService
{
    public interface ITeamsService
    {
        Task<Pagination<TeamsViewModel>> GetAllTeams(string? searchQuery, int pageNumber, int pageSize);
        Task<TeamsViewModel> GetTeam(int id);
        Task CreateTeams(CreateTeamsViewModel createTeams);
        Task UpdateTeams(int id, UpdateTeamsViewModel updateTeams);
        Task DeleteTeamsPhysical(int id);
        Task DeleteTeamsVirtual(int id);
    }
}
