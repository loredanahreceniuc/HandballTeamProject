using exp.NET6.Infrastructure.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.ArticleGallerys
{
    public  interface IArticleGalleryRepository:IGenericRepository<ArticleGallery>
    {
        Task<ArticleGallery> DeleteVirtual(int id);
    }
}
