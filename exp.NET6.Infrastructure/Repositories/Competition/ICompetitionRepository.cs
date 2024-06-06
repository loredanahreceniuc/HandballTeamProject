using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Competitions
{
    public interface ICompetitionRepository : IGenericRepository<Competition>
    {
        Task<Competition> DeleteVirtual(int id);
    }
}
