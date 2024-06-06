using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.PlayerCategories;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Players
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(FlowerPowerDbContext context) : base(context) { }

        public IQueryable<Player> GetPlayerDetails()
        {
            return context.Players.Include(x => x.PlayerGalleries).AsNoTracking().AsQueryable();
            //return context.Players.AsNoTracking().AsQueryable();
        }
        public async Task<Player> DeleteVirtual(int id)
        {
            try
            {
                var entity = await context.Set<Player>().FindAsync(id);
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
    }
}
