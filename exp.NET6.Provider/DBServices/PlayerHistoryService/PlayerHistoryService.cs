using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.PlayerHistories;
using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.PlayerHistoryService;
using MailKit.Search;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.PlayerHistoryService
{
    public class PlayerHistoryService : IPlayerHistoryService
    {
        private readonly IPlayerHistoryRepository _playerhistoryRepository;
        private readonly IGenericService _genericService;

        public PlayerHistoryService(IPlayerHistoryRepository playerhistoryRepository, IGenericService genericService)
        {
            _playerhistoryRepository = playerhistoryRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<PlayerHistoryViewModel>> GetAllPlayerHistory(string? searchQuery, int pageSize, int pageNumber)
        {
            var pagination = new Pagination<PlayerHistoryViewModel>();
            var playerHistory = _playerhistoryRepository.GetAllQuerable();

            var itemCount = await playerHistory.CountAsync();


            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                playerHistory = playerHistory.Where(x => x.Team.Contains(searchQuery));
            }
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await playerHistory.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new PlayerHistoryViewModel()
            {
                Id = x.Id,
                Team=x.Team,
                Season=x.Season,
                Results=x.Results,
                Position=x.Position,
                PlayerId=x.PlayerId,
            }).ToListAsync();



            return pagination;
        }

        public async Task<PlayerHistoryViewModel> GetPlayerHistory(int id)
        {
            var playerhistory = await _playerhistoryRepository.Get(id);

            if (playerhistory == null)
            {
                throw new KeyNotFoundException("This player does not exist!");
            }
            var returnPlayerHistory = new PlayerHistoryViewModel()
            {
                Id = playerhistory.Id,
                Team=playerhistory.Team,
                Season=playerhistory.Season,
                Results=playerhistory.Results,
                Position=playerhistory.Position,
                PlayerId=playerhistory.PlayerId,
              
            };
            return returnPlayerHistory;
        }

        public async Task CreatePlayerHistory(CreatePlayerHistoryViewModel createPlayerHistory)
        {
            var addPlayerHistory = new PlayerHistory()
            {
                Team=createPlayerHistory.Team,
                Season=createPlayerHistory.Season,
                Results=createPlayerHistory.Results,
                Position=createPlayerHistory.Position,
                PlayerId=createPlayerHistory.PlayerId,

             
            };
            await _playerhistoryRepository.Add(addPlayerHistory);
        }

        public async Task UpdatePlayerHistory(int id, UpdatePlayerHistoryViewModel updatePlayerHistory)
        {
            var playerhistory = await _playerhistoryRepository.Get(id);
            if (playerhistory == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }
         playerhistory.Team = updatePlayerHistory.Team;
            playerhistory.Season = updatePlayerHistory.Season;
            playerhistory.Results = updatePlayerHistory.Results;
            playerhistory.Position = updatePlayerHistory.Position;
            playerhistory.PlayerId = updatePlayerHistory.PlayerId;
            await _playerhistoryRepository.Update(playerhistory);
        }

        public async Task DeletePlayerHistoryPhysical(int id)
        {
            var playerhistory = await _playerhistoryRepository.Get(id);
            if (playerhistory == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }

            await _playerhistoryRepository.DeleteVirtual(id);
        }

        public async Task DeletePlayerHistoryVirtual(int id)
        {
            var playerhistory = await _playerhistoryRepository.Get(id);

            if (playerhistory == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }
            await _playerhistoryRepository.DeleteVirtual(id);
        }
    }
}
