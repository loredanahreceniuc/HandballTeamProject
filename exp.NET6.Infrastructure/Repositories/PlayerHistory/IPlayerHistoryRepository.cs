using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.PlayerHistories
{
    public interface IPlayerHistoryRepository : IGenericRepository<PlayerHistory>
    {
        Task<PlayerHistory> DeleteVirtual(int id);
    }
}
