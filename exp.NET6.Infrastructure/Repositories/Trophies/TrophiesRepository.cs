using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Trophies
{
    public class TrophiesRepository : GenericRepository<Trophy>, ITrophiesRepository
    {
        public TrophiesRepository(FlowerPowerDbContext context) : base(context) { }


        public async Task<Trophy> DeleteVirtual(int id)
        {
            try
            {
                var entity = await context.Set<Trophy>().FindAsync(id);
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
