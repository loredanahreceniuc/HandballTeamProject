using exp.NET6.Infrastructure.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.BlogCategories
{
    public interface IBlogCategoryRepository : IGenericRepository<BlogCategory>
    {
        Task<BlogCategory> DeleteVirtual(int id);
    }
}
