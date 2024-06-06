using exp.net6.backend.Auth;
using exp.net6.backend.Auth.Models;
using exp.net6.backend.Helpers;
using exp.NET6.Infrastructure.Repositories.UserLocations;
using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices;
using exp.NET6.Services.DBServices.UserService;
using exp.NET6.Services.DBServices.UserService.UserLocationService;
using exp.NET6.Services.Email;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Org.BouncyCastle.Crypto.Fpe;

using SQLitePCL;

namespace exp.net6.backend.Controllers.Auth
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin,User")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IGenericService _genericService;
        private readonly IEmailService _emailService;
        public readonly IUserLocationService _userLocationService;
        public UserController(IConfiguration iconfig, UserManager<ApplicationUser> userManager, IUserService userService,
            IGenericService genericService, IEmailService emailService, IUserLocationService userLocationService)
        {
            _userManager = userManager;
            _userService = userService;
            _genericService = genericService;
            _emailService = emailService;
            _userLocationService = userLocationService;
        }

        [HttpGet("getAllUsers")]
        public IActionResult GetUsers(string? role, string? searchText, string? status, string? orderBy, int pageSize = 10, int pageNumber = 1)
        {
            var users = _userService.GetUsers(role, searchText, status, orderBy, pageSize, pageNumber);
            return Ok(users);
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUsers(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var simpleUser = new SimpleUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
            };

            return Ok(simpleUser);
        }

        [HttpGet("getUserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetailsController(string userId)
        {
            var user = await _userService.GetUserDetails(userId);

            return Ok(user);
        }

        [HttpGet("getLocation/{id}")]
        public async Task<IActionResult> GetLocationController(int id)
        {
            var location = await _userLocationService.GetLocation(id);

            return Ok(location);
        }

        [HttpGet("getClients")]
        public async Task<IActionResult> GetClientsController(bool showActiveUsers, string? searchQuery, string? orderBy, int pageSize = 10, int pageNumber = 1)
        {
            var clients = await _userService.GetClients(showActiveUsers, searchQuery, orderBy, pageSize, pageNumber);

            return Ok(clients);
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel updatedUser)
        {
            var user = await _userManager.FindByIdAsync(updatedUser.Id);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.UserName = updatedUser.Email;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;

            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            if (updatedUser.Role != null && userRole != updatedUser.Role)
            {
                if (userRole != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole);
                }
                await _userManager.AddToRoleAsync(user, updatedUser.Role);
            }
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "User update failed! Please check user details and try again.");

            return Ok("User updated successfully!");
        }

        [HttpPut("updateClient/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateClientViewModel updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.UserName = updatedUser.Email;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;


            var result = await _userManager.UpdateAsync(user);

            if (updatedUser.Shipping != null)
                await _userLocationService.UpdateUserLocation(updatedUser.Shipping);
            if (updatedUser.Billing != null)
                await _userLocationService.UpdateUserLocation(updatedUser.Billing);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "User update failed! Please check user details and try again.");

            return Ok("User updated successfully!");
        }

        [HttpPut("changeUserAccess/{id}")]
        public async Task<IActionResult> UpdateUserAccess(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.LockoutEnabled = !user.LockoutEnabled;
            await _userManager.UpdateAsync(user);

            if (!user.LockoutEnabled)
            {
                return Ok("User has been blocked!");
            }
            else
            {
                return Ok("User has been granted access!");
            }
        }


        [HttpPut("updateUserLocation")]
        public async Task<IActionResult> UpdateUserLocationController(UpdateUserLocationViewModel updateUserLocation)
        {
            await _userLocationService.UpdateUserLocation(updateUserLocation);

            return NoContent();
        }


        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(CreateUser createUser)
        {
            var userExists = await _userManager.FindByEmailAsync(createUser.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "User already exists!");
            }

            var userNameExists = await _userManager.FindByNameAsync(createUser.Email);
            if (userNameExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Username already exists!");
            }

            ApplicationUser user = new()
            {
                UserName = createUser.Email,
                PhoneNumber = createUser.PhoneNumber,
                Email = createUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now,
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                IsDeleted = false,
            };
            var password = _genericService.GeneratePassword();
            var result = await _userManager.CreateAsync(user, password);

          
            if (createUser.Role == null)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, createUser.Role);
            }
            var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //if (user.Email != null)
            //    await _emailService.SendComfirmEmailUser(user.Email, password, emailConfirmationCode, Request.Headers["origin"]);
            sendVerificationEmailWithPassword(user.Email, password, emailConfirmationCode, Request.Headers["origin"]);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.");

            return Ok("User created successfully!");
        }

        [HttpPost("createClient")]
        public async Task<IActionResult> CreateClientController(CreateClientViewModel createClient)
        {
            var userExists = await _userManager.FindByEmailAsync(createClient.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "User already exists!");
            }

            var userNameExists = await _userManager.FindByNameAsync(createClient.Email);
            if (userNameExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Username already exists!");
            }

            ApplicationUser user = new()
            {
                UserName = createClient.Email,
                PhoneNumber = createClient.PhoneNumber,
                Email = createClient.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now,
                FirstName = createClient.FirstName,
                LastName = createClient.LastName,
                IsDeleted = false,
            };
            var password = _genericService.GeneratePassword();
            var result = await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, "Client");

            if (createClient.Shipping != null)
            {
                createClient.Shipping.Title = "Shipping address";
                await _userLocationService.CreateUserLocation(user.Id, createClient.Shipping);
            }
            if (createClient.Billing != null)
            {
                createClient.Billing.Title = "Billing address";
                await _userLocationService.CreateUserLocation(user.Id, createClient.Billing);
            }

            var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //if (user.Email != null)
            //    await _emailService.SendComfirmEmailClient(user.Email, password, emailConfirmationCode);
            sendVerificationEmailWithPassword(user.Email, password, emailConfirmationCode, Request.Headers["origin"]);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.");

            return Ok("User created successfully!");
        }


        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);

            return Ok("User has been deleted!");
        }

        private async void sendVerificationEmailWithPassword(string Email, string Password, string VerificationToken, string origin)
        {
            string message;
            var verifyUrl = $"{origin}/account/verify-email?email={Email}&token={VerificationToken}";
            message = $@"<p>Your password is {Password} feel free to change it whenever you want. <p/>
                        <p>Please click the below link to verify your email address:</p>
                        <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";

            _emailService.Send(
                to: Email,
                subject: "Sign-up Verification API",
                html: $@"<h4>Reset Password Email</h4>
                        {message}"
            );
        }

    }
}
