using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class TeamCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; } 
        public string? ImgBase64 { get; set; }
    }

    public class CreateTeamCategoryViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImgBase64 { get; set; }
    }

    public class UpdateTeamCategoryViewModel: CreateTeamCategoryViewModel { }
}
