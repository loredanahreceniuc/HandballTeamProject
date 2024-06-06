using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.PlayerService
{
    public interface IPlayerService
    {

            Task<Pagination<PlayersViewModel>> GetAllPlayer(bool? mainTeamValue, string? position, int pageSize, int pageNumber);
            Task<PlayerViewModel> GetPlayer(int id);
            Task CreatePlayer(CreatePlayerViewModel createPlayer);
            Task UpdatePlayer(int id, UpdatePlayerViewModel updatePlayer);
            Task DeletePlayerPhysical(int id);
            Task DeletePlayerVirtual(int id);
    }
}
