using exp.net6.backend.Auth.Models;
using exp.net6.backend.Auth;
using exp.NET6.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using exp.net6.backend.Helpers;
using System.Security.Principal;
using System.Text.RegularExpressions;
using exp.NET6.Services.DBServices;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using exp.NET6.Services.DBServices.UserService.UserLocationService;
using exp.NET6.Models.ViewModel;

namespace exp.net6.backend.Controllers.Auth
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IGenericService _genericService;
        private readonly IUserLocationService _userLocationService;
        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IEmailService emailService,
            IGenericService genericService,
            IUserLocationService userLocationService
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailService = emailService;
            _genericService = genericService;
            _userLocationService = userLocationService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "User already exists!");
            }

            var userNameExists = await _userManager.FindByNameAsync(model.Email);
            if (userNameExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Username already exists!");
            }

            if (model.PhoneNumber != null)
            {
                if (_genericService.ValidatePhoneNumber(model.PhoneNumber) == false)
                {
                    throw new AppException("Phone number format is not valid");
                }
            }
            var nameParts = model.Name.Split(' ');
            var firstName = nameParts[0];
            var lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;
            if(String.IsNullOrWhiteSpace(firstName) || String.IsNullOrWhiteSpace(lastName))
            {
                throw new AppException("Name is not valid");
            }
            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true, //TODO
                CreatedDate = DateTime.Now,
                PhoneNumber = model.PhoneNumber,
                FirstName = firstName,
                LastName = lastName,
                IsDeleted = false,
                
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.");

            await _userManager.AddToRoleAsync(user, "Admin");
        
            var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (user.Email != null)
                sendVerificationEmail(user.Email, emailConfirmationCode, Request.Headers["origin"]);
           
            return Ok("User created successfully!");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            //var role = await _userManager.IsInRoleAsync(user,"Admin");
            if (user == null || user.IsDeleted == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Login failed! User not found!");
            }

            if (user.LockoutEnabled == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Login failed! User is blocked!");
            }

            var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!emailConfirmed)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Login failed! In order to log in, please confirm the email.");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Login failed! Wrong password!");
            }

            if (user != null && checkPassword)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                //user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(1);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    UserId = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
                });
            }

            return Unauthorized();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token!");
            }

            string username = principal.GetUserName();
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now) /*user.RefreshToken != refreshToken ||*/
            {
                return BadRequest("Invalid access token or refresh token!");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
            });
        }

        [AllowAnonymous]
        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ValidateEmail validate)
        {
            if (validate.Email != null && validate.Code != null)
            {
                var user = await _userManager.FindByEmailAsync(validate.Email);
                if (user == null)
                {
                    return BadRequest("The user was not found!");
                }

                var result = await _userManager.ConfirmEmailAsync(user, validate.Code);
                if (result.Succeeded)
                {
                    return Ok("Email confirmed successfully!");
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            if (User.Identity is null)
            {
                return BadRequest("Invalid user name");
            }
            else
            {
                string userName = User.Identity.Name ?? string.Empty;

                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return BadRequest("Invalid user name");
                }
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _userManager.UpdateAsync(user);
            }

            return Ok("Refresh token for logged user revoked successfully!");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await Revoke();
            await _signInManager.SignOutAsync();
            return Ok("Logout successfully!");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return BadRequest("Invalid user!");
                }

                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, code }, protocol: Request.Scheme);
                //_emailService.Send(user.Email, "Companies portal - Reset pasword", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                sendPasswordResetEmail(user.Email, code, Request.Headers["origin"]);
                return Ok("Email with password reset code has been sent successfully!");
            }

            return BadRequest("Something wrong!");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something wrong!");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Invalid user!");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Something wrong!");
            }

            return Ok("Password has been successfully updated!");
        }

        [HttpGet]
        [Authorize]
        [Route("userDetails")]
        public IActionResult GetUserDetailsByAccesToken()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);

            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(accessToken, validations, out var tokenSecure);

            string userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            return new ObjectResult(new
            {
                UserId = claims.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName = claims.FindFirstValue(ClaimTypes.Name),
                Email = claims.FindFirstValue(ClaimTypes.Email),
                UserRoles = claims.FindAll(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
            });
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var test = tokenValidityInMinutes;

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(3),
                //expires: DateTime.Now.AddMinutes(0.2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false,
                //ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
                    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private void sendVerificationEmail(string Email, string VerificationToken, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?email={Email}&token={VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                            <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                            <p><code>{VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                        <p>Thanks for registering!</p>
                        {message}"
            );
        }
        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                        <p>Your email <strong>{email}</strong> is already registered.</p>
                        {message}"
            );
        }

        private void sendPasswordResetEmail(string Email, string ResetToken, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/Homepage?email={Email}&token={ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                            <p><code>{ResetToken}</code></p>";
            }

            _emailService.Send(
                to: Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                        {message}"
            );
        }


    }
}
