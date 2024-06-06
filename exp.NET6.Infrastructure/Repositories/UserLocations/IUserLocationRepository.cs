using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.UserLocations
{
    public interface IUserLocationRepository: IGenericRepository<UserLocation>
    {
        Task<UserLocation> DeleteVirtual(int id);
        Task<UserLocation?> GetUserLocationAsync(string id);
    }
}
