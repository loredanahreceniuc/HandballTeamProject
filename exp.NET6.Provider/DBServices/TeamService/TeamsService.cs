
using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.Teams;
using Microsoft.EntityFrameworkCore;
using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Search;

namespace exp.NET6.Services.DBServices.TeamService
{
    public class TeamsService : ITeamsService
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly IGenericService _genericService;

        public TeamsService(ITeamsRepository teamsRepository, IGenericService genericService)
        {
            _teamsRepository = teamsRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<TeamsViewModel>> GetAllTeams(string? searchQuery, int pageNumber, int pageSize)
        {
            var pagination = new Pagination<TeamsViewModel>();
            var teams = _teamsRepository.GetAllQuerable();

            var itemCount = await teams.CountAsync();

            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                teams = teams.Where(x => x.Name.Contains(searchQuery));
            }
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await teams.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new TeamsViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl),
                Wins = x.Wins,
                Losses =x.Losses,
                Draw = x.Draw,
                GoalDifference = x.GoalDifference,
                Points =x.Points,
            }).ToListAsync();

            return pagination;
        }
        public async Task<TeamsViewModel> GetTeam(int id)
        {
            var teams = await _teamsRepository.Get(id);

            if (teams == null)
            {
                throw new KeyNotFoundException("This team does not exist!");
            }
            var returnTeams = new TeamsViewModel()
            {
                Id = teams.Id,
                Name = teams.Name,
                ImgBase64 = _genericService.GetImageFormat(teams.ImgUrl),
                Wins = teams.Wins,
                Losses = teams.Losses,
                Draw = teams.Draw,
                GoalDifference = teams.GoalDifference,
                Points = teams.Points,
            
            };
            return returnTeams;
        }
        public async Task CreateTeams(CreateTeamsViewModel createTeams)
        {
            var addTeams = new Team()
            {
                Name = createTeams.Name,
                ImgUrl = _genericService.GetImagePath(createTeams.ImgBase64, null, "teams"),
                Wins =createTeams.Wins,
                Losses = createTeams.Losses,
                Draw = createTeams.Draw,
                GoalDifference=createTeams.GoalDifference,
                Points = createTeams.Points,
            };
            await _teamsRepository.Add(addTeams);
        }

        public async Task UpdateTeams(int id, UpdateTeamsViewModel updateTeams)
        {
            var teams = await _teamsRepository.Get(id);
            if (teams == null)
            {
                throw new KeyNotFoundException("This team does not exist");
            }

            updateTeams.Name = teams.Name;
            updateTeams.ImgBase64 = _genericService.GetImagePath(updateTeams.ImgBase64, teams.ImgUrl, "teams");
            updateTeams.Wins = teams.Wins;
            updateTeams.Losses = teams.Losses;
            updateTeams.Draw = teams.Draw;
            updateTeams.GoalDifference = teams.GoalDifference;
            updateTeams.Points = teams.Points;
            await _teamsRepository.Update(teams);
        }
        public async Task DeleteTeamsPhysical(int id)
        {
            var teams = await _teamsRepository.Get(id);
            if (teams == null)
            {
                throw new KeyNotFoundException("This team does not exist");
            }

            await _teamsRepository.DeleteVirtual(id);
        }

        public async Task DeleteTeamsVirtual(int id)
        {
            var teams = await _teamsRepository.Get(id);

            if (teams == null)
            {
                throw new KeyNotFoundException("This team does not exist");
            }
            await _teamsRepository.DeleteVirtual(id);
        }

    }
}
