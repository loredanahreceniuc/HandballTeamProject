using exp.NET6.Infrastructure.Entities;
using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.PlayerGalleryService
{
    public interface IPlayerGalleryService
    {
        Task<Pagination<PlayerGalleryViewModel>> GetAllPlayerGallery(int pageNumber, int pageSize);
        Task<PlayerGalleryViewModel> GetPlayerGallery(int id);
        Task CreatePlayerGallery(CreatePlayerGalleryViewModel createPlayerGallery);
        Task UpdatePlayerGallery(int id, UpdatePlayerGalleryViewModel updatePlayerGallery);
        Task DeletePlayerGalleryPhysical(int id);
        Task DeletePlayerGalleryVirtual(int id);
        Task DeletePlayersRange(List<PlayerGallery> gallery);
    }
}
