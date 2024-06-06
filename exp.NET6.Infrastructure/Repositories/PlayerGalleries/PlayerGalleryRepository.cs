using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;

using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.PlayerGalleries
{
    public class PlayerGalleryRepository : GenericRepository<PlayerGallery>, IPlayerGalleryRepository
    {
        public PlayerGalleryRepository(FlowerPowerDbContext context) : base(context) { }

        public async Task<PlayerGallery> DeleteVirtual(int id)
        {
            try
            {
                var entity = await context.Set<PlayerGallery>().FindAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException("This object does not exist, please enter a valid id");

                entity.IsDeleted = true;
                await context.SaveChangesAsync();

                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PlayerGallery>> RemoveGalleryRange(IEnumerable<PlayerGallery> entities)
        {
            foreach(var entity in entities)
            {
                if (entity.ImgUrl != null)
                    File.Delete(Directory.GetCurrentDirectory() + "\\Images" + entity.ImgUrl);
            }
            
            context.Set<PlayerGallery>().RemoveRange(entities);
            await context.SaveChangesAsync();
            return entities;
        }
    }
}
