using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.TeamRanking;
using exp.NET6.Models.ViewModel;

using MailKit.Search;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.TeamRankingService
{
    public class TeamRankingService: ITeamRankingService
    {
        private readonly IGenericService _genericService;
        private readonly ITeamRankingRepository _teamRankingRepository;
        public TeamRankingService(ITeamRankingRepository teamRankingRepository, IGenericService genericService)
        {
            _genericService = genericService;
            _teamRankingRepository =  teamRankingRepository;
        }

        public async Task<Pagination<TeamRankingViewModel>> GetAllTeamRanking(string? searchQuery, int pageSize, int pageNumber, bool orderMatches)
        {
            var pagination = new Pagination<TeamRankingViewModel>();
            var teamRanking = _teamRankingRepository.GetAllQuerable();

            var itemCount = await teamRanking.CountAsync();

            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                teamRanking = teamRanking.Where(x => x.Name.Contains(searchQuery));
            }

            if(orderMatches)
            {
                teamRanking = teamRanking.OrderBy(x => x.Points);

            }
            else
            {
                teamRanking=teamRanking.OrderByDescending(x=>x.Points);
            }



            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await teamRanking.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new TeamRankingViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                GamesPlayed = x.GamesPlayed,
                Wins = x.Wins,
                Draws = x.Draws,
                Losses = x.Losses,
                Goals = x.Goals,
                Points = x.Points,
                ImgBase64=_genericService.GetImageFormat(x.ImgUrl)

            }).ToListAsync();

            return pagination;
        }

        public async Task<TeamRankingViewModel> GetTeamRanking(int id)
        {
            var teamRanking = await _teamRankingRepository.Get(id);

            if (teamRanking == null)
            {
                throw new KeyNotFoundException("This team ranking does not exist!");
            }
            var returnTeamRanking = new TeamRankingViewModel()
            {
                Id = teamRanking.Id,
                Name = teamRanking.Name,
                GamesPlayed = teamRanking.GamesPlayed,
                Wins = teamRanking.Wins,
                Draws = teamRanking.Draws,
                Losses = teamRanking.Losses,
                Goals = teamRanking.Goals,
                Points = teamRanking.Points,
                ImgBase64=_genericService.GetImageFormat(teamRanking.ImgUrl)

            };

            return returnTeamRanking;
        }

        public async Task CreateTeamRanking(CreateTeamRankingViewModel createTeamRanking)
        {
            var addTeamRanking = new TeamsRanking()
            {
                Name = createTeamRanking.Name,
                GamesPlayed = createTeamRanking.GamesPlayed,
                Wins = createTeamRanking.Wins,
                Draws = createTeamRanking.Draws,
                Losses = createTeamRanking.Losses,
                Goals = createTeamRanking.Goals,
                Points = createTeamRanking.Points,
                ImgUrl=_genericService.GetImagePath(createTeamRanking.ImgBase64,null,"teamRanking")
            };

            await _teamRankingRepository.Add(addTeamRanking);
        }

        public async Task UpdateTeamRanking(int id, UpdateTeamRankingViewModel updateTeamRanking)
        {
            var teamRanking = await _teamRankingRepository.Get(id);

            if (teamRanking == null)
            {
                throw new KeyNotFoundException("This team ranking does not exist");
            }

            teamRanking.Name = updateTeamRanking.Name;
            teamRanking.GamesPlayed = updateTeamRanking.GamesPlayed;
            teamRanking.Wins = updateTeamRanking.Wins;
            teamRanking.Draws = updateTeamRanking.Draws;
            teamRanking.Losses = updateTeamRanking.Losses;
            teamRanking.Goals = updateTeamRanking.Goals;
            teamRanking.Points = updateTeamRanking.Points;
            teamRanking.ImgUrl = _genericService.GetImagePath(updateTeamRanking.ImgBase64, teamRanking.ImgUrl, "teamRanking");

            await _teamRankingRepository.Update(teamRanking);
        }

        public async Task DeleteTeamRankingPhysical(int id)
        {
            var teamRanking = await _teamRankingRepository.Get(id);

            if (teamRanking == null)
            {
                throw new KeyNotFoundException("This team ranking does not exist");
            }
            if(teamRanking.ImgUrl != null)
                File.Delete(Directory.GetCurrentDirectory() + "\\Images" + teamRanking.ImgUrl);

            await _teamRankingRepository.DeletePhysical(id);
        }

        public async Task DeleteTeamRankingVirtual(int id)
        {
            var teamRanking = await _teamRankingRepository.Get(id);

            if (teamRanking == null)
            {
                throw new KeyNotFoundException("This team ranking does not exist");
            }

            teamRanking.IsDeleted = true;

            if(teamRanking.ImgUrl != null)
                File.Delete(Directory.GetCurrentDirectory()+ "\\Images" + teamRanking.ImgUrl);

            await _teamRankingRepository.Update(teamRanking);
        }
    }
}

