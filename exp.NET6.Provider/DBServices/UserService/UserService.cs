using exp.NET6.Infrastructure.Repositories.User;
using exp.NET6.Infrastructure.Repositories.UserLocations;
using exp.NET6.Models.ViewModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Pagination<UserViewModel> GetUsers(string? role, string? searchText, string? status, string? orderBy, int pageSize, int pageNumber)
        {
            var pagination = new Pagination<UserViewModel>();
            var users = _userRepository.GetUsers().Where(x => !x.Roles.Select(r => r.Name).Contains("Client"));

            if (!String.IsNullOrEmpty(searchText))
                users = users.Where(x => x.FirstName.Contains(searchText) || x.LastName.Contains(searchText) || x.Email.Contains(searchText));

            if (!String.IsNullOrWhiteSpace(role))
            {
                users = users.Where(x => x.Roles.Any(x => x.Name == role));
            }

            if (!String.IsNullOrWhiteSpace(status))
            {
                users = users.Where(x => x.LockoutEnabled == (status == "Active"));
            }

            users = orderBy switch
            {
                "name" => users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName),
                "name_desc" => users.OrderByDescending(x => x.FirstName).ThenBy(x => x.LastName),
                "email_confimation" => users.OrderBy(x => x.EmailConfirmed),
                "email_confimation_desc" => users.OrderByDescending(x => x.EmailConfirmed),
                _ => users.OrderBy(x => x.UserName),
            };

            var itemCount = users.Count();
            pagination.Items = users.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new UserViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                PhoneNumber = x.PhoneNumber,
                AccountCreatedDate = x.CreatedDate,
                Role = x.Roles.FirstOrDefault().Name,
                IsBlocked = x.LockoutEnabled,
            }).ToList();

            pagination.PageDetails = new PageDetails
            {
                PageSize = pageSize,
                TotalItemCount = itemCount,
                TotalPageCount = (int)Math.Ceiling(itemCount / (double)pageSize),
            };

            return pagination;
        }

        public async Task<UserDetails> GetUserDetails(string userId)
        {
            var user = await _userRepository.GetUsers().Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (user == null)
                throw new KeyNotFoundException("This user does not exist please provide a valid one!");

            return new UserDetails()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccountCreatedDate = user.CreatedDate,
                IsBlocked = user.LockoutEnabled,
                PhoneNumber = user.PhoneNumber,
                Role = user.Roles.Select(x => x.Name).FirstOrDefault(),
                FidelityPoints = user.FidelityPoints == null ? 0 : user.FidelityPoints,
                Locations = user.UserLocations.Select(x => new UserLocationViewModel()
                {
                    Id = x.Id,
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    State = x.State,
                    Title = x.Title,
                }).ToList(),
            };
        }

        public async Task<Pagination<ClientsViewModel>> GetClients(bool showActiveUsers, string? searchQuery, string? orderBy, int pageSize, int pageNumber)
        {
            var pagination = new Pagination<ClientsViewModel>();
            var clients = _userRepository.GetUsers().Where(x => x.Roles.Select(r => r.Name).Contains("Client"));


            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                clients = clients.Where(x => x.FirstName.Contains(searchQuery) || x.LastName.Contains(searchQuery) || x.Email.Contains(searchQuery));
            }

            clients = clients.Where(x => x.LockoutEnabled == showActiveUsers);
            clients = orderBy switch
            {
                "name" => clients.OrderBy(x => x.FirstName).ThenBy(x => x.LastName),
                "name_desc" => clients.OrderByDescending(x => x.FirstName).ThenBy(x => x.LastName),
                "phone_number" => clients.OrderBy(x => x.PhoneNumber),
                "phone_number_desc" => clients.OrderByDescending(x => x.PhoneNumber),
                /*"comments" => clients.OrderBy(x => x.Reviews.Select(c=> c.Comment.Count())),
                "comments_desc" => clients.OrderByDescending(x => x.Reviews.Count()),*/
                "fidelity_points" => clients.OrderBy(x => x.FidelityPoints),
                "fidelity_points_desc" => clients.OrderByDescending(x => x.FidelityPoints),
                _ => clients.OrderBy(x => x.UserName),
            };

            var itemCount = clients.Count();
            pagination.PageDetails = new PageDetails
            {
                PageSize = pageSize,
                TotalItemCount = itemCount,
                TotalPageCount = (int)Math.Ceiling(itemCount / (double)pageSize),
            };

            pagination.Items = await clients.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new ClientsViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.LockoutEnabled,
                FidelityPoints = x.FidelityPoints == null ? 0 : (int)x.FidelityPoints,
            }).ToListAsync();

            return pagination;
        }

        public async Task<List<UserList>> GetUserList()
        {
            var users = _userRepository.GetUsers();

            return await users.Select(x => new UserList()
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
            }).ToListAsync();
        }
    }
}
