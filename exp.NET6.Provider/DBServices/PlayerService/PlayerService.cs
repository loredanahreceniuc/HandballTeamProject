using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.PlayerCategories;
using exp.NET6.Infrastructure.Repositories.Players;
using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.PlayerGalleryService;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGenericService _genericService;
        private readonly IPlayerGalleryService _playerGalleryService;
        public PlayerService(IPlayerRepository playerRepository, IGenericService genericService, IPlayerGalleryService playerGalleryService)
        {
            _playerRepository = playerRepository;
            _genericService = genericService;
            _playerGalleryService = playerGalleryService;
        }

        public async Task<Pagination<PlayersViewModel>> GetAllPlayer(bool? mainTeamValue, string? position,int pageSize, int pageNumber)
        {
            var pagination = new Pagination<PlayersViewModel>();
            var players = _playerRepository.GetAllQuerable();

            if (!String.IsNullOrWhiteSpace(position))
            {
                players = players.Where(x => x.Position == position);
            }

            if(mainTeamValue != null)
            {
                players= players.Where( x=> x.MainTeam == mainTeamValue);
            }



            var itemCount = await players.CountAsync();
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await players.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new PlayersViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate,
                Height = x.Height,
                Weight = x.Weight,
                Description = x.Description,
                Position    = x.Position,
                Biography = x.Biography,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl),
                Nationality = x.Nationality,
                CategoryId = x.CategoryId,    
                Number = x.Number,
                MainTeam = x.MainTeam
            }).ToListAsync();
            return pagination;
        }

        public async Task<PlayerViewModel> GetPlayer(int id)
        {
            var player = await _playerRepository.GetPlayerDetails().Where(x=> x.Id == id).FirstOrDefaultAsync();

            if (player == null)
            {
                throw new KeyNotFoundException("This player does not exist!");
            }
            var returnPlayer = new PlayerViewModel()
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                BirthDate = player.BirthDate.ToString("yyyy-MM-dd"),
                Height= player.Height,
                Weight = player.Weight, 
                Description = player.Description,
                Position = player.Position,
                Biography = player.Biography,
                ImgBase64 = _genericService.GetImageFormat(player.ImgUrl),
                Nationality = player.Nationality,
                CategoryId = player.CategoryId,
                Number = player.Number,
                MainTeam=player.MainTeam,
                ImgGallery = player.PlayerGalleries.Select(x => _genericService.GetImageFormat(x.ImgUrl)).ToList(),
            };
            return returnPlayer;
        }

        public async Task CreatePlayer(CreatePlayerViewModel createPlayer)
        {
            if(createPlayer.Number != null)
            {
                var players = await _playerRepository.GetAllQuerable().FirstOrDefaultAsync(x => x.Number == createPlayer.Number);
                if(players != null)
                {
                    throw new ArgumentException("Player number already used");
                }
            }

            if(createPlayer.Number != null && createPlayer.Number <= 0)
            {
                throw new ArgumentException("Player number is not valid used");

            }

            if (createPlayer.Height <= 0)
            {
                throw new ArgumentException("Player Height is not valid used");

            }

            if (createPlayer.Weight <= 0)
            {
                throw new ArgumentException("Player Weight is not valid used");

            }
            var addPlayer = new Player()
            {
                Number = createPlayer.Number,
                FirstName = createPlayer.FirstName,
                LastName = createPlayer.LastName,
                BirthDate = createPlayer.BirthDate,
                Height = createPlayer.Height,
                Weight = createPlayer.Weight,
                Description = createPlayer.Description,
                Position = createPlayer.Position,
                Biography= createPlayer.Biography,
                ImgUrl = _genericService.GetImagePath(createPlayer.ImgBase64, null, "player"),
                Nationality = createPlayer.Nationality,
                CategoryId = createPlayer.CategoryId,
                MainTeam = createPlayer.MainTeam,
            };
            var playerInfo = await _playerRepository.Add(addPlayer);

            if(createPlayer.ImgGallery != null)
            {
                foreach (var img in createPlayer.ImgGallery)
                {
                    await _playerGalleryService.CreatePlayerGallery(new CreatePlayerGalleryViewModel()
                    {
                        ImgBase64 = img,
                        PlayerId = playerInfo.Id
                    });
                }
            }
          
        }

        public async Task UpdatePlayer(int id, UpdatePlayerViewModel updatePlayer)
        {
            var player = await _playerRepository.GetPlayerDetails().FirstOrDefaultAsync(x => x.Id == id);
            if (player == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }

            if (updatePlayer.Number != null)
            {
                var players = await _playerRepository.GetAllQuerable().FirstOrDefaultAsync(x => x.Number == updatePlayer.Number);
                if (players != null && players.Id != id)
                {
                    throw new ArgumentException("Player number already used");
                }
            }

            if (updatePlayer.Number != null && updatePlayer.Number <= 0)
            {
                throw new ArgumentException("Player number is not valid used");

            }

            if (updatePlayer.Height <= 0)
            {
                throw new ArgumentException("Player Height is not valid used");

            }

            if (updatePlayer.Weight <= 0)
            {
                throw new ArgumentException("Player Weight is not valid used");

            }

            player.FirstName = updatePlayer.FirstName;
            player.LastName = updatePlayer.LastName;
            player.BirthDate = updatePlayer.BirthDate;
            player.Height = updatePlayer.Height;
            player.Weight = updatePlayer.Weight;
            player.Description = updatePlayer.Description;
            player.Position = updatePlayer.Position;
            player.ImgUrl = _genericService.GetImagePath(updatePlayer.ImgBase64, player.ImgUrl, "player");
            player.Nationality = updatePlayer.Nationality;
            player.CategoryId = updatePlayer.CategoryId;
            player.Number = updatePlayer.Number;
            player.MainTeam = updatePlayer.MainTeam;
            await _playerRepository.Update(player);

            await _playerGalleryService.DeletePlayersRange(player.PlayerGalleries.ToList());

            if (updatePlayer.ImgGallery != null)
            {
                foreach (var img in updatePlayer.ImgGallery)
                {
                    await _playerGalleryService.CreatePlayerGallery(new CreatePlayerGalleryViewModel()
                    {
                        ImgBase64 = img,
                        PlayerId = player.Id
                    });
                }
            }
        }

        public async Task DeletePlayerPhysical(int id)
        {
            var player = await _playerRepository.Get(id);
            if (player == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }

            await _playerRepository.DeletePhysical(id);
        }

        public async Task DeletePlayerVirtual(int id)
        {
            var player = await _playerRepository.Get(id);

            if (player == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }

            player.IsDeleted = true;
            await _playerRepository.Update(player);
        }
    }
}
