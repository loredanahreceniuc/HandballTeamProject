using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.UserService.UserLocationService
{
    public interface IUserLocationService
    {
        Task<UserLocationViewModel> GetLocation(int id);
        Task UpdateUserLocation(UpdateUserLocationViewModel updateUserLocation);
        Task CreateUserLocation(string userId, CreateUserLocationViewModel createUserLocation);
    }
}
