using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.SponsorService
{
    public interface ISponsorService
    {
        Task<Pagination<SponsorViewModel>> GetAllSponsors(string? searchQuery, int pageNumber, int pageSize);
        Task<SponsorViewModel> GetSponsor(int id);
        Task CreateSponsor(CreateSponsorViewModel createSponsor);
        Task UpdateSponsor(int id, UpdateSponsorViewModel updateSponsor);
        Task DeleteSponsor(int id);
    }
}
