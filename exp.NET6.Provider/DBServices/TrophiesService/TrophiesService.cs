using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.Trophies;
using exp.NET6.Models.ViewModel;

using MailKit.Search;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.TrophiesService
{
    public class TrophiesService : ITrophiesService
    {
        private readonly ITrophiesRepository _trophiesRepository;
        private readonly IGenericService _genericService;

        public TrophiesService(ITrophiesRepository trophiesRepository, IGenericService genericService)
        {
            _trophiesRepository = trophiesRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<TrophiesViewModel>> GetAllTrophies(string? searchQuery, int pageNumber, int pageSize)
        {
            var pagination = new Pagination<TrophiesViewModel>();
            var trophies = _trophiesRepository.GetAllQuerable();

            var itemCount = await trophies.CountAsync();

            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                trophies = trophies.Where(x => x.Name.Contains(searchQuery));
            }
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await trophies.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new TrophiesViewModel()
            {
                Id = x.Id,
                Name= x.Name,
                Date = x.Date.ToString("dd/MM/yyyy"),
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl),
            }).ToListAsync();

            return pagination;
        }
        public async Task<TrophyViewModel> GetTrophies(int id)
        {
            var trophies = await _trophiesRepository.Get(id);

            if (trophies == null)
            {
                throw new KeyNotFoundException("This trophy does not exist!");
            }
            var returnTrophies = new TrophyViewModel()
            {
                Id = trophies.Id,
                Name = trophies.Name,
                Date = trophies.Date.ToString("yyyy-MM-dd"),
                ImgBase64 = _genericService.GetImageFormat(trophies.ImgUrl),
            };
            return returnTrophies;
        }
        public async Task CreateTrophies(CreateTrophiesViewModel createTrophies)
        {
            var addTrophies = new Trophy()
            {
                Name = createTrophies.Name,
                Date = DateTime.Now,
                ImgUrl = _genericService.GetImagePath(createTrophies.ImgBase64, null, "trophy")
            };
            await _trophiesRepository.Add(addTrophies);
        }

        public async Task UpdateTrophies(int id, UpdateTrophiesViewModel updateTrophies)
        {
            var trophies = await _trophiesRepository.Get(id);
            if (trophies == null)
            {
                throw new KeyNotFoundException("This trophy does not exist");
            }

            trophies.Name = updateTrophies.Name;
            trophies.Date = updateTrophies.Date;
            trophies.ImgUrl = _genericService.GetImagePath(updateTrophies.ImgBase64, trophies.ImgUrl, "trophy");

            await _trophiesRepository.Update(trophies);
        }
        public async Task DeleteTrophiesPhysical(int id)
        {
            var trophies = await _trophiesRepository.Get(id);
            if (trophies == null)
            {
                throw new KeyNotFoundException("This trophy does not exist");
            }

            await _trophiesRepository.DeleteVirtual(id);
        }

        public async Task DeleteTrophiesVirtual(int id)
        {
            var trophies = await _trophiesRepository.Get(id);

            if (trophies == null)
            {
                throw new KeyNotFoundException("This trophy does not exist");
            }

            trophies.IsDeleted = true;
            await _trophiesRepository.Update(trophies);
        }

    }
}
