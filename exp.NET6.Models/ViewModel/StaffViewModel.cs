using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class StaffViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string? Role { get; set; }
        public string? ImgBase64 { get; set; }
    }


    public class CreateStaffViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [MaxLength(100)]
        public string? Role { get; set; }
        public string? ImgBase64 { get; set; }

    }
    public class UpdateStaffViewModel : CreateStaffViewModel { }
}

