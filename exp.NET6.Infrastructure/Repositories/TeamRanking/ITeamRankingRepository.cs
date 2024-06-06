using exp.NET6.Infrastructure.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.TeamRanking
{
    public interface ITeamRankingRepository: IGenericRepository<TeamsRanking>
    {
        Task<TeamsRanking> DeleteVirtual(int id);
    }
}
