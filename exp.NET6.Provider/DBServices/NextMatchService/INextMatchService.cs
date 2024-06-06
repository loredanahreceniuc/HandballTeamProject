using exp.NET6.Infrastructure.Entities;
using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.NextMatchService
{
    public interface INextMatchService
    {
        Task<Pagination<NextMatchViewModel>> GetAllNextMatch(string? searchQuery, int pageNumber, int pageSize);
        Task<NextMatchViewModel> GetNextMatch(int id);
        Task CreateNextMatch(CreateNextMatchViewModel createNextMatch);
        Task UpdateNextMatch(int id, UpdateNextMatchViewModel updateNextMatch);
        Task DeleteNextMatchPhysical(int id);
        Task DeleteNextMatchVirtual(int id);


    }
}
