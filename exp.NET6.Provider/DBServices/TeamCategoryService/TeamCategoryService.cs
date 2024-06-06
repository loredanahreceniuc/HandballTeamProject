using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.TeamCategories;
using exp.NET6.Models.ViewModel;

using MailKit.Search;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.TeamCategoryService
{
    public class TeamCategoryService : ITeamCategoryService
    {
        private readonly ITeamCategoryRepository _teamCategoryRepository;
        private readonly IGenericService _genericService;

        public TeamCategoryService(ITeamCategoryRepository teamCategoryRepository, IGenericService genericService)
        {
            _teamCategoryRepository = teamCategoryRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<TeamCategoryViewModel>> GetAllTeamCategories(string? searchQuery, int pageSize, int pageNumber)
        {
            var pagination = new Pagination<TeamCategoryViewModel>();
            var teamCategories = _teamCategoryRepository.GetAllQuerable();
            var itemCount = await teamCategories.CountAsync();

            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                teamCategories = teamCategories.Where(x => x.Title.Contains(searchQuery));
            }

            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await teamCategories.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new TeamCategoryViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl),
                Title = x.Title,
            }).ToListAsync();

            return pagination;
        }

        public async Task<TeamCategoryViewModel> GetTeamCategory(int id)
        {
            var teamCategory = await _teamCategoryRepository.Get(id);

            if (teamCategory == null)
            {
                throw new KeyNotFoundException("This team category does not exist!");
            }
            var returnTeamCategory = new TeamCategoryViewModel()
            {
                Id = teamCategory.Id,
                Description = teamCategory.Description,
                ImgBase64 = _genericService.GetImageFormat(teamCategory.ImgUrl),
                Title = teamCategory.Title,
            };

            return returnTeamCategory;
        }

        public async Task CreateTeamCategory(CreateTeamCategoryViewModel createteamCategory)
        {
            var addteamCategory = new TeamCategory()
            {
                Description = createteamCategory.Description,
                Title = createteamCategory.Title,
                ImgUrl = _genericService.GetImagePath(createteamCategory.ImgBase64, null, "TeamCategory")
            };

            await _teamCategoryRepository.Add(addteamCategory);
        }

        public async Task UpdateTeamCategory(int id, UpdateTeamCategoryViewModel updateTeamCategory)
        {
            var teamCategory = await _teamCategoryRepository.Get(id);

            if(teamCategory == null)
            {
                throw new KeyNotFoundException("This team category does not exist");
            }

            teamCategory.Description= updateTeamCategory.Description;
            teamCategory.Title = updateTeamCategory.Title;
            teamCategory.ImgUrl = _genericService.GetImagePath(updateTeamCategory.ImgBase64, teamCategory.ImgUrl, "TeamCategory");

            await _teamCategoryRepository.Update(teamCategory);
        }

        public async Task DeleteTeamCategoryPhysical(int id)
        {
            var teamCategory = await _teamCategoryRepository.Get(id);

            if (teamCategory == null)
            {
                throw new KeyNotFoundException("This team category does not exist");
            }

            await _teamCategoryRepository.DeletePhysical(id);
        }

        public async Task DeleteTeamCategoryVirtual(int id)
        {
            var teamCategory = await _teamCategoryRepository.Get(id);

            if (teamCategory == null)
            {
                throw new KeyNotFoundException("This team category does not exist");
            }

            await _teamCategoryRepository.DeleteVirtual(id);
        }
    }
}
