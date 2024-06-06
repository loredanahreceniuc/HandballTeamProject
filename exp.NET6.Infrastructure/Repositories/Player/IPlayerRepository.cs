using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.PlayerCategories
{
    public interface IPlayerRepository:IGenericRepository<Player>
    {
        Task<Player> DeleteVirtual(int id);
        IQueryable<Player?> GetPlayerDetails();
    }
}
