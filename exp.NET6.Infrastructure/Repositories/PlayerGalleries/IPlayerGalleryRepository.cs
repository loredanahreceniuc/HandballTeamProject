using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.PlayerGalleries
{
    public interface IPlayerGalleryRepository:IGenericRepository<PlayerGallery>
    {
        Task<PlayerGallery> DeleteVirtual(int id);
        Task<IEnumerable<PlayerGallery>> RemoveGalleryRange(IEnumerable<PlayerGallery> entities);
    }
}
