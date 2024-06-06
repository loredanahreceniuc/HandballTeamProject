using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.Sponsors;
using exp.NET6.Models.ViewModel;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.SponsorService
{
    public class SponsorService: ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IGenericService _genericService;
        public SponsorService(ISponsorRepository sponsorRepository, IGenericService genericService)
        {
            _sponsorRepository = sponsorRepository ?? throw new ArgumentNullException(nameof(sponsorRepository));
            _genericService = genericService ?? throw new ArgumentNullException();
        }

        public async Task<Pagination<SponsorViewModel>> GetAllSponsors(string? searchQuery, int pageNumber, int pageSize)
        {
            var pagination = new Pagination<SponsorViewModel>();
            var sponsors = _sponsorRepository.GetAllQuerable();

            if (!String.IsNullOrEmpty(searchQuery))
            {
                sponsors = sponsors.Where(x => x.Name.Contains(searchQuery));
            }

            int itemCount = await sponsors.CountAsync();
            pagination.PageDetails = new PageDetails()
            {
                PageSize = pageSize,
                TotalItemCount = itemCount,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await sponsors.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new SponsorViewModel()
            {
                Id= x.Id,
                Name = x.Name,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl),
                Link= x.Link,
            }).ToListAsync();

            return pagination;
        }

        public async Task<SponsorViewModel> GetSponsor(int id)
        {
            var sponsor = await _sponsorRepository.GetAllQuerable().Where(x=> x.Id == id).FirstOrDefaultAsync();

            if(sponsor == null)
            {
                throw new KeyNotFoundException("This sponsor does not exist");
            }
            var returnSponsor = new SponsorViewModel()
            {
                Id = sponsor.Id,
                Name = sponsor.Name,
                ImgBase64 = _genericService.GetImageFormat(sponsor.ImgUrl),
                Link = sponsor.Link,
            };

            return returnSponsor;
        }

        public async Task CreateSponsor(CreateSponsorViewModel createSponsor)
        {
            var sponsor = new Sponsor()
            {
                ImgUrl = _genericService.GetImagePath(createSponsor.ImgBase64, null, "Sponsors"),
                Link = createSponsor.Link,
                Name = createSponsor.Name,
            };

            await _sponsorRepository.Add(sponsor);
        }

        public async Task UpdateSponsor(int id, UpdateSponsorViewModel updateSponsor)
        {

            var sponsor = await _sponsorRepository.Get(id);

            if(sponsor == null)
            {
                throw new KeyNotFoundException("This sponsor does not exist");
            }

            sponsor.ImgUrl = _genericService.GetImagePath(updateSponsor.ImgBase64, null, "Sponsors");
            sponsor.Name = updateSponsor.Name;
            sponsor.Link = updateSponsor.Link;

            await _sponsorRepository.Update(sponsor);
        }

        public async Task DeleteSponsor(int id)
        {

            var sponsor = await _sponsorRepository.Get(id);

            if (sponsor == null)
            {
                throw new KeyNotFoundException("This sponsor does not exist");
            }

            sponsor.IsDeleted = true;
            await _sponsorRepository.Update(sponsor);
        }
    }
}
