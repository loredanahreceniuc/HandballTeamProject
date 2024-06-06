using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.PlayerGalleries;
using exp.NET6.Services.DBServices.PlayerGalleryService;
using exp.NET6.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.PlayerGalleryService
{
    public class PlayerGalleryService : IPlayerGalleryService
    {
        private readonly IPlayerGalleryRepository _playerGalleryRepository;
        private readonly IGenericService _genericService;

        public PlayerGalleryService(IPlayerGalleryRepository playerGalleryRepository, IGenericService genericService)
        {
            _playerGalleryRepository = playerGalleryRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<PlayerGalleryViewModel>> GetAllPlayerGallery(int pageNumber, int pageSize)
        {
            var pagination = new Pagination<PlayerGalleryViewModel>();
            var playerGallery = _playerGalleryRepository.GetAllQuerable();

            var itemCount = await playerGallery.CountAsync();
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };


            pagination.Items = await playerGallery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new PlayerGalleryViewModel()
            {
                Id = x.Id,
                PlayerId = x.PlayerId,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl)
            }).ToListAsync();




            return pagination;
        }

        public async Task<PlayerGalleryViewModel> GetPlayerGallery(int id)
        {
            var playerGallery = await _playerGalleryRepository.Get(id);

            if (playerGallery == null)
            {
                throw new KeyNotFoundException("This player does not exist. ");
            }

            var returnPlayerGallery = new PlayerGalleryViewModel()
            {
                Id = playerGallery.Id,
                PlayerId = playerGallery.PlayerId,
                ImgBase64 = _genericService.GetImageFormat(playerGallery.ImgUrl),
            };

            return returnPlayerGallery;
        }

        public async Task CreatePlayerGallery(CreatePlayerGalleryViewModel createPlayerGallery)
        {
            var addPlayerGallery = new PlayerGallery()
            {
                PlayerId = createPlayerGallery.PlayerId,
                ImgUrl = _genericService.GetImagePath(createPlayerGallery.ImgBase64, null, "playerGallery"),
            };
            await _playerGalleryRepository.Add(addPlayerGallery);
        }

        public async Task UpdatePlayerGallery(int id, UpdatePlayerGalleryViewModel updatePlayerGallery)
        {
            var playerGallery = await _playerGalleryRepository.Get(id);
            if (playerGallery != null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }
            playerGallery.ImgUrl = _genericService.GetImagePath(updatePlayerGallery.ImgBase64, playerGallery.ImgUrl, "playerGallery");
            await _playerGalleryRepository.Update(playerGallery);
        }

        public async Task DeletePlayerGalleryPhysical(int id)
        {
            var playerGallery = await _playerGalleryRepository.Get(id);
            if (playerGallery == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }
            if (playerGallery.ImgUrl != null)
                File.Delete(Directory.GetCurrentDirectory() + "\\Images" + playerGallery.ImgUrl);

            await _playerGalleryRepository.DeletePhysical(id);
        }

        public async Task DeletePlayerGalleryVirtual(int id)
        {
            var playerGallery = await _playerGalleryRepository.Get(id);

            if (playerGallery == null)
            {
                throw new KeyNotFoundException("This player does not exist");
            }

            playerGallery.IsDeleted = true;
            if(playerGallery.ImgUrl != null)
                File.Delete(Directory.GetCurrentDirectory() + "\\Images" + playerGallery.ImgUrl);

            await _playerGalleryRepository.Update(playerGallery);
        }

        public async Task DeletePlayersRange(List<PlayerGallery> gallery)
        {
            await _playerGalleryRepository.RemoveGalleryRange(gallery);
        }

    }
}
