using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.User
{
    public class UserRepository : GenericRepository<AspNetUser>, IUserRepository
    {

        public UserRepository(FlowerPowerDbContext context) : base(context) { }

        public IQueryable<AspNetUser?> GetUsers()
        {
            return context.AspNetUsers
                .Include(x => x.UserLocations)
                .Include(x => x.Roles)
                .AsNoTracking().AsQueryable();
        }

        public async Task<AspNetUser?> GetUser(string id)
        {
            return await context.AspNetUsers.Where(x => x.Id == id)
                .Include(x => x.UserLocations)
                .FirstOrDefaultAsync();
        }

        public async Task<AspNetUser?> GetUserOrderDetails(string id)
        {
            return await context.AspNetUsers.Where(x => x.Id == id)
                .Include(x => x.UserLocations)
                .FirstOrDefaultAsync();
        }
        public async Task<AspNetUser?> GetUserDetails(string id)
        {
            return await context.AspNetUsers
                .Include(x => x.UserLocations)
                .Include(x => x.Roles)
                .Where(x=> x.Id == id).FirstOrDefaultAsync();
        }
    }
}
