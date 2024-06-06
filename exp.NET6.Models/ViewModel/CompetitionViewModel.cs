using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class CompetitionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImgBase64 { get; set; }
    }
    public class CreateCompetitionViewModel
    {

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImgBase64 { get; set; }

    }

    public class UpdateCompetitionViewModel : CreateCompetitionViewModel
    {
        public int Id { get; set; }
    }
}
