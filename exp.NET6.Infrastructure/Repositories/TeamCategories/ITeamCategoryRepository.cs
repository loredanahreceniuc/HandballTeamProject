using exp.NET6.Infrastructure.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.TeamCategories
{
    public interface ITeamCategoryRepository: IGenericRepository<TeamCategory>
    {
        Task<TeamCategory> DeleteVirtual(int id);
    }
}
