using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace exp.NET6.Models.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? AccountCreatedDate { get; set; }
        public string? Role { get; set; }
        public bool? IsBlocked { get; set; }
    }

    public class SimpleUserViewModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }

    public class UserDetails
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? AccountCreatedDate { get; set; }
        public string? Role { get; set; }
        public bool? IsBlocked { get; set; }
        public int ReviewsCount { get; set; }
        public int OrdersCount { get; set; }
        public int ProductsBought { get; set; }
        public int? FidelityPoints { get; set; }


        public List<UserLocationViewModel>? Locations { get; set; }
    }
    public class UpdateUserViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9 _-]*$",
            ErrorMessage = "The firstName must consists of 2 to 30 characters inclusive, can only have alphanumeric characters, underscores (_) and hyphens (-)!")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9 _-]*$",
           ErrorMessage = "The lastName must consists of 2 to 30 characters inclusive, can only have alphanumeric characters, underscores (_) and hyphens (-)!")]
        [Display(Name = "LastName ")]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string? Email { get; set; } = null!;
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }

    }

    public class UpdateUserAccess
    {
        [Required]
        public string UserId { get; set; }

        public bool IsBlocked { get; set; }
    }


    public class UserLocationViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string UserId { get; set; } = null!;
    }

    public class UpdateUserLocationViewModel
    {
        public string? Title { get; set; }
        public int? Id { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }
    }

    public class CreateUserLocationViewModel
    {
        public string? Title { get; set; }
        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

    }

    public class ClientsViewModel
    {
        public string Id { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Reviews { get; set; }
        //public string? Comments { get; set; }
        public int FidelityPoints { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateClientViewModel
    {
        [Required(ErrorMessage = "Client first name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Client last name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Client email is required")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public CreateUserLocationViewModel? Shipping { get; set; }
        public CreateUserLocationViewModel? Billing { get; set; }
    }

    public class UpdateClientViewModel
    {
        [Required(ErrorMessage = "Client first name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Client last name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Client email is required")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public UpdateUserLocationViewModel? Shipping { get; set; }
        public UpdateUserLocationViewModel? Billing { get; set; }
    }

    public class UserList
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
