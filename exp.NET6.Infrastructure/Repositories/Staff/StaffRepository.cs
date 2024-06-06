using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.StaffCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Staffs
{
    public class StaffRepository : GenericRepository<staff>, IStaffRepository
    {
        public StaffRepository(FlowerPowerDbContext context) : base(context) { }


        public async Task<staff> DeleteVirtual(int id)
        {
            try
            {
                var entity = await context.Set<staff>().FindAsync(id);
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


