using exp.NET6.Infrastructure.Entities;
using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace exp.NET6.Services.DBServices.ClubDetailsService
{
    public interface IClubDetailsService
    {
        Task<ClubDetailsViewModel> GetClubDetails();
        Task UpdateClubDetails(string? content);
    }
}
