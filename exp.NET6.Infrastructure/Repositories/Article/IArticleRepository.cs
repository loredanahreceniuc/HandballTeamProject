using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Articles
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        Task<Article> DeleteVirtual(int id);
    }
}
