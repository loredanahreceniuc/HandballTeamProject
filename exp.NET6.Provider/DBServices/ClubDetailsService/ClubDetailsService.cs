using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.ClubDetails;
using exp.NET6.Models.ViewModel;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.ClubDetailsService
{
    public class ClubDetailsService : IClubDetailsService
    {
        private readonly IClubDetailsRepository _clubDetailsRepository;
        private readonly IGenericService _genericService;

        public ClubDetailsService(IClubDetailsRepository clubDetailsRepository, IGenericService genericService)
        {
            _clubDetailsRepository = clubDetailsRepository;
            _genericService = genericService;
        }


        public async Task<ClubDetailsViewModel> GetClubDetails()
        {
            var clubDetails = await _clubDetailsRepository.GetAllQuerable().FirstOrDefaultAsync();

            if (clubDetails == null)
            {
                return new ClubDetailsViewModel()
                {
                    Content = "",
                };
            }

            var returnClubDetails = new ClubDetailsViewModel()
            {
                Content = clubDetails.Content,
            };

            return returnClubDetails;
        }

        public async Task UpdateClubDetails(string? content)
        {
            var clubDetails = await _clubDetailsRepository.GetAllQuerable().FirstOrDefaultAsync();

            if (clubDetails == null)
            {
                var addClubDetails = new ClubDetail()
                {
                    Content = content,

                };
                await _clubDetailsRepository.Add(addClubDetails);
            }
            else
            {
                clubDetails.Content = content;
                await _clubDetailsRepository.Update(clubDetails);
            }
        }
    }
}
