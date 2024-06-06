using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.User;
using exp.NET6.Infrastructure.Repositories.UserLocations;
using exp.NET6.Models.ViewModel;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.UserService.UserLocationService
{
    public class UserLocationService :IUserLocationService
    {
        private readonly IUserLocationRepository _userLocationRepository;
        private readonly IUserRepository _userRepository;
        public UserLocationService(IUserLocationRepository userLocationRepository, IUserRepository userRepository)
        {
            _userLocationRepository = userLocationRepository;
            _userRepository = userRepository;
        }

        public async Task<UserLocationViewModel> GetLocation(int id)
        {
            var user = await _userLocationRepository.Get(id);

            if(user== null)
            {
                throw new KeyNotFoundException("This Location does not exist");
            }

            return new UserLocationViewModel()
            {
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                Id = user.Id,
                State = user.State,
                Title = user.Title,
                UserId = user.UserId,
            };
        }

        public async Task UpdateUserLocation(UpdateUserLocationViewModel updateUserLocation)
        {
            var location = await _userLocationRepository.Get(updateUserLocation.Id !=null ? (int)updateUserLocation.Id : 0);

            if(location == null)
            {
                throw new ArgumentException("This location does not exist, please provide a valid one");
            }

            location.State = updateUserLocation.State;
            location.Address = updateUserLocation.Address;
            location.City = updateUserLocation.City;
            location.Country = updateUserLocation.Country;

            await _userLocationRepository.Update(location);
        }

        public async Task CreateUserLocation(string UserId, CreateUserLocationViewModel location)
        {
            var user = await _userRepository.GetUser(UserId);

            if (user == null)
            {
                throw new KeyNotFoundException("This user does not exist, something went wrong");
            }

            await _userLocationRepository.Add(new UserLocation()
            {
                Address = location.Address,
                City = location.City,
                Country = location.Country,
                State = location.State,
                Title = location.Title,
                UserId = user.Id,
            });
        }
    }
}
