using System;
using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.CompetitionService
{
    public interface ICompetitionService
    {
        Task<Pagination<CompetitionViewModel>> GetAllCompetition(string? searchQuery, int pageNumber, int pageSize);
        Task<CompetitionViewModel> GetCompetition(int id);
        Task CreateCompetition(CreateCompetitionViewModel createCompetition);
        Task UpdateCompetition(int id, UpdateCompetitionViewModel updateCompetition);
        Task DeleteCompetitionPhysical(int id);
        Task DeleteCompetitionVirtual(int id);
    }
}

