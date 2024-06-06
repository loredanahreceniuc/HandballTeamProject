using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.UserLocations
{
    public class UserLocationRepository: GenericRepository<UserLocation>, IUserLocationRepository
    {
        public UserLocationRepository(FlowerPowerDbContext context) : base(context) { }

        public async Task<UserLocation> DeleteVirtual(int id)
        {
            try
            {
                var entity = await context.Set<UserLocation>().FindAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException("This product does not exist, please enter a valid product");

                entity.IsDeleted = true;
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserLocation?> GetUserLocationAsync(string id)
        {
            return await context.UserLocations.Where(x=> x.UserId == id).FirstOrDefaultAsync();
        }
    }
}
