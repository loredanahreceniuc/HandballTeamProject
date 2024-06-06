using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.PlayerHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.PlayerHistories
{
    public class PlayerHistoryRepository : GenericRepository<PlayerHistory>, IPlayerHistoryRepository
    {
        public PlayerHistoryRepository(FlowerPowerDbContext context) : base(context) { }


        public async Task<PlayerHistory> DeleteVirtual(int id)
        {
            try
            {
                var entity = await context.Set<PlayerHistory>().FindAsync(id);
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


