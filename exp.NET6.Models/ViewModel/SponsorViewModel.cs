using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class SponsorViewModel
    {
        public int Id { get; set; }
        public string? ImgBase64 { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
    }

    public class CreateSponsorViewModel
    {
        public string? ImgBase64 { get; set; }
        [Required(ErrorMessage = "Sponsor name is required")]
        [MaxLength(300)]
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
    }

    public class UpdateSponsorViewModel:CreateSponsorViewModel { }
}
