using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.User
{
    public interface IUserRepository : IGenericRepository<AspNetUser>
    {
        IQueryable<AspNetUser?> GetUsers();
        Task<AspNetUser?> GetUser(string id);
        Task<AspNetUser?> GetUserDetails(string id);
        Task<AspNetUser?> GetUserOrderDetails(string id);
    }
}
