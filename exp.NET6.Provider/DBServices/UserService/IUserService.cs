using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.UserService
{
    public interface IUserService
    {
        Pagination<UserViewModel> GetUsers(string? role, string? searchText, string? status, string? orderBy, int pageSize, int pageNumber);
        Task<UserDetails> GetUserDetails(string userId);
        Task<Pagination<ClientsViewModel>> GetClients(bool showActiveUsers, string? searchQuery, string? orderBy, int pageSize, int pageNumber);
    }
}
