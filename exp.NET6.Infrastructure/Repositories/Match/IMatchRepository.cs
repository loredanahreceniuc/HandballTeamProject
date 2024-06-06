using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Matches
{
    public interface IMatchRepository: IGenericRepository<Match>
    {
        Task<Match> DeleteVirtual(int id);
        IQueryable<Match> GetMatchesQueryable();
    }
}
