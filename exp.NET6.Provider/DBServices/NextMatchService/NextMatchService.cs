using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.NextMatches;
using exp.NET6.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.NextMatchService
{
    public class NextMatchService : INextMatchService
    {
        private readonly INextMatchRepository _nextMatchRepository;
        private readonly IGenericService _genericService;

        public NextMatchService(INextMatchRepository nextMatchRepository, IGenericService genericService)
        {
            _nextMatchRepository = nextMatchRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<NextMatchViewModel>> GetAllNextMatch(string? searchQuery, int pageSize, int pageNumber)
        {
            var pagination = new Pagination<NextMatchViewModel>();
            var nextMatches = _nextMatchRepository.GetAllQuerable();
            var itemCount = await nextMatches.CountAsync();

            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                nextMatches = nextMatches.Where(x => x.Hosts.Contains(searchQuery) || x.Guests.Contains(searchQuery));
            }

            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await nextMatches.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new NextMatchViewModel()
            {

                MatchDate = x.MatchDate,
                Hosts = x.Hosts,
                Guests = x.Guests,
            }).ToListAsync();

            return pagination;
        }

        public async Task<NextMatchViewModel> GetNextMatch(int id)
        {
            var nextMatch = await _nextMatchRepository.Get(id);

            if (nextMatch == null)
            {
                throw new KeyNotFoundException("This next match does not exist. ");
            }

            var returnNextMatch = new NextMatchViewModel()
            {
                MatchDate = nextMatch.MatchDate,
                Hosts = nextMatch.Hosts,
                Guests = nextMatch.Guests
            };

            return returnNextMatch;
        }
        public async Task CreateNextMatch(CreateNextMatchViewModel createNextMatch)
        {
            var addNextMatch = new NextMatch()
            {
                MatchDate = createNextMatch.MatchDate,
                Hosts = createNextMatch.Hosts,
                Guests = createNextMatch.Guests
            };
            await _nextMatchRepository.Add(addNextMatch);
        }

        public async Task UpdateNextMatch(int id, UpdateNextMatchViewModel updateNextMatch)
        {
            var nextMatch = await _nextMatchRepository.Get(id);

            if (nextMatch == null)
            {
                throw new KeyNotFoundException("This next match does not exist");
            }

            nextMatch.MatchDate = updateNextMatch.MatchDate;
            nextMatch.Hosts = updateNextMatch.Hosts;
            nextMatch.Guests = updateNextMatch.Guests;

            await _nextMatchRepository.Update(nextMatch);
        }
        public async Task DeleteNextMatchPhysical(int id)
        {
            var nextMatch = await _nextMatchRepository.Get(id);

            if (nextMatch == null)
            {
                throw new KeyNotFoundException("This next match does not exist");
            }

            await _nextMatchRepository.DeletePhysical(id);
        }

        public async Task DeleteNextMatchVirtual(int id)
        {
            var nextMatch = await _nextMatchRepository.Get(id);

            if (nextMatch == null)
            {
                throw new KeyNotFoundException("This next match does not exist");
            }

            await _nextMatchRepository.DeleteVirtual(id);
        }
    }
}

    





