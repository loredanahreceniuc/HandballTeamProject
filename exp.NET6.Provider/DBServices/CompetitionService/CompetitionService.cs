using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.Competitions;
using exp.NET6.Models.ViewModel;
using MailKit.Search;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.CompetitionService
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IGenericService _genericService;

        public CompetitionService(ICompetitionRepository competitionRepository, IGenericService genericService)
        {
            _competitionRepository = competitionRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<CompetitionViewModel>> GetAllCompetition(string? searchQuery, int pageSize, int pageNumber)
        {
            var pagination = new Pagination<CompetitionViewModel>();
            var competition = _competitionRepository.GetAllQuerable();

            var itemCount = await competition.CountAsync();
            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                competition = competition.Where(x => x.Name.Contains(searchQuery));
            }
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await competition.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new CompetitionViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return pagination;
        }

        public async Task<CompetitionViewModel> GetCompetition(int id)
        {
            var competition = await _competitionRepository.Get(id);

            if (competition == null)
            {
                throw new KeyNotFoundException("This competition does not exist!");
            }
            var returnCompetition = new CompetitionViewModel()
            {
                Id = competition.Id,
                Name = competition.Name,

            };

            return returnCompetition;
        }

        public async Task CreateCompetition(CreateCompetitionViewModel createcompetition)
        {
            var addcompetition = new Competition()
            {
                Name = createcompetition.Name,


            };

            await _competitionRepository.Add(addcompetition);
        }

        public async Task UpdateCompetition(int id, UpdateCompetitionViewModel updateCompetition)
        {
            var competition = await _competitionRepository.Get(id);

            if (competition == null)
            {
                throw new KeyNotFoundException("This competition does not exist");
            }

            await _competitionRepository.Update(competition);
        }

        public async Task DeleteCompetitionPhysical(int id)
        {
            var competition = await _competitionRepository.Get(id);

            if (competition == null)
            {
                throw new KeyNotFoundException("This competition does not exist");
            }

            await _competitionRepository.DeletePhysical(id);
        }

        public async Task DeleteCompetitionVirtual(int id)
        {
            var competition = await _competitionRepository.Get(id);

            if (competition == null)
            {
                throw new KeyNotFoundException("This competition does not exist");
            }

            await _competitionRepository.DeleteVirtual(id);
        }
    }
}
