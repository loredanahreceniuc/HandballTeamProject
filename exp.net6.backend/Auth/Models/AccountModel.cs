using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace exp.net6.backend.Auth.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9 _-]*$",
            ErrorMessage = "The firstName must consists of 2 to 30 characters inclusive, can only have alphanumeric characters, underscores (_) and hyphens (-)!")]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&_]).{8,}$",
            ErrorMessage = "Passwords must have at least one non alphanumeric character, one digit ('0'-'9'), one uppercase ('A'-'Z') and one lowercase ('a'-'z')!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }
    }

    public class CreateUser
    {
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
        [Display(Name = "LastName")]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }

    }
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public string? Code { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }
    }

    public class ValidateEmail
    {
        [Required(ErrorMessage = "Email si required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Code si required")]
        public string Code { get; set; } = null!;
    }
}
