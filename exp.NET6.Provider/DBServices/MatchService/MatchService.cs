using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.Competitions;
using exp.NET6.Infrastructure.Repositories.Matches;
using exp.NET6.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.MatchService
{
    public class MatchService: IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IGenericService _genericService;
        private readonly ICompetitionRepository _competitionRepository;

        public MatchService(IMatchRepository matchRepository, IGenericService genericService, ICompetitionRepository competitionRepository)
        {
            _matchRepository = matchRepository;
            _genericService = genericService;
            _competitionRepository = competitionRepository;
        }


        public async Task<Pagination<MatchViewModel>>GetAllMatch(string? searchQuery, DateTime? date, int? competitionId,  int pageNumber, int pageSize)
        {
            var pagination = new Pagination<MatchViewModel>();
            var matches = _matchRepository.GetAllQuerable();

            var itemCount= await matches.CountAsync();

            if(!String.IsNullOrEmpty(searchQuery) )
            {
                matches = matches.Where(x=> x.Home.Contains(searchQuery) || x.Away.Contains(searchQuery));
            }

            if(date != null)
            {
                matches = matches.Where(x => x.Date.Date <= date.Value.Date);
            }

            if(competitionId != null)
            {
                matches = matches.Where(x => x.CompetitionId == competitionId);
            }

            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items= await matches.Skip(pageSize * (pageNumber-1)).Take(pageSize).Select(x => new MatchViewModel()
            {
                Id= x.Id,
                IsHome = x.Home.ToLower().Contains("suceava") ? true : false,
                Date= x.Date.ToString("MMMM yyyy"),
                MatchDate = x.Date.ToString("dd HH:mm"),
                Home = x.Home,
                Away=x.Away,
                HomePoints= x.HomePoints,
                AwayPoints= x.AwayPoints,
                CompetitionId= new MathCompetition()
                {
                    Id = x.Competition != null ? x.Competition.Id : null,
                    Name = x.Competition != null ? x.Competition.Name : null,
                },
                HomeImgBase64= _genericService.GetImageFormat(x.HomeImgUrl),
                AwayImgBase64=_genericService.GetImageFormat(x.AwayImgUrl),
               
            }).ToListAsync();

            return pagination;
        }



        public async Task<MatchViewModel> GetMatch(int id)
        {
            var matches = await _matchRepository.GetAllQuerable().FirstOrDefaultAsync(x=> x.Id == id);
            if(matches == null)
            {
                throw new KeyNotFoundException("This match does not exist.");
            }

            var returnmatch = new MatchViewModel()
            {
                Id = matches.Id,
                Date = matches.Date.ToString(),
                Home = matches.Home,
                Away = matches.Away,
                HomePoints = matches.HomePoints,
                AwayPoints = matches.AwayPoints,
                CompetitionId = new MathCompetition()
                {
                    Id = matches.Competition != null ? matches.Competition.Id : null,
                    Name = matches.Competition != null ? matches.Competition.Name : null,
                },
                HomeImgBase64 = _genericService.GetImageFormat(matches.HomeImgUrl),
                AwayImgBase64 = _genericService.GetImageFormat(matches.AwayImgUrl),

            };

            return returnmatch;
        }


        public async Task CreateMatch(CreateMatchViewModel createMatch)
        {
            if(createMatch.CompetitionId != null)
            {
                var competition = await _competitionRepository.Get((int)createMatch.CompetitionId);

                if (competition == null)
                    throw new KeyNotFoundException("This competition does not exist");
            }

            var addmatch = new Match()
            {
                Date = createMatch.Date,
                Home = createMatch.Home,
                Away = createMatch.Away,
                HomePoints = createMatch.HomePoints,
                AwayPoints = createMatch.AwayPoints,
                CompetitionId = createMatch.CompetitionId,
                HomeImgUrl = _genericService.GetImagePath(createMatch.HomeImgBase64, null, "Match"),
                AwayImgUrl = _genericService.GetImagePath(createMatch.AwayImgBase64, null, "Match"),
            };
            await _matchRepository.Add(addmatch);
        }


        public async Task UpdateMatch(int id, UpdateMatchViewModel updateMatch)
        {
            if (updateMatch.CompetitionId != null)
            {
                var competition = await _competitionRepository.Get((int)updateMatch.CompetitionId);

                if (competition == null)
                    throw new KeyNotFoundException("This competition does not exist");
            }

            var matches = await _matchRepository.Get(id);
            if(matches == null)
            {
                throw new KeyNotFoundException("This match does not exist");
            }

            matches.HomeImgUrl= _genericService.GetImagePath(updateMatch.HomeImgBase64,matches.HomeImgUrl, "match");
            matches.AwayImgUrl=_genericService.GetImagePath(updateMatch.AwayImgBase64,matches.AwayImgUrl, "match");
            await _matchRepository.Update(matches);
        }



        public async Task DeleteMatchPhysical(int id)
        {
            var matches = await _matchRepository.Get(id);
            if (matches == null)
            {
                throw new KeyNotFoundException("This match does not exist");
            }
            if (matches.HomeImgUrl != null)
                File.Delete(Directory.GetCurrentDirectory() + "\\Images" + matches.HomeImgUrl);
            if(matches.AwayImgUrl != null)
            File.Delete(Directory.GetCurrentDirectory() + "\\Images" + matches.AwayImgUrl);

            await _matchRepository.DeletePhysical(id);
        }


        public async Task DeleteMatchVirtual(int id)
        {
            var matches = await _matchRepository.Get(id);

            if(matches == null)
            {
                throw new KeyNotFoundException("This match does not exist");

            }

            matches.IsDeleted = true;
            if(matches.HomeImgUrl != null)
                File.Delete(Directory.GetCurrentDirectory() + "\\Images" + matches.HomeImgUrl);
            if (matches.AwayImgUrl != null)
                File.Delete(Directory.GetCurrentDirectory()+ "\\Images" + matches.AwayImgUrl);

            await _matchRepository.Update(matches);

        }



    }
}
