using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class TeamsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public string? ImgBase64 { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? Draw {  get; set; }
        public int? GoalDifference {  get; set; }
        public int? Points {  get; set; }
    }
    public class CreateTeamsViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string? Name { get; set; }
        public string? ImgBase64 { get; set; }

        [Required(ErrorMessage = "Wins are required")]
        public int? Wins { get; set; }

        [Required(ErrorMessage = "Losses is required")]
        public int? Losses { get; set; }

        [Required(ErrorMessage = "Draw is required")]
        public int? Draw { get; set; }
        [Required(ErrorMessage = "Goal difference  is required")]
        public int? GoalDifference { get; set; }
        [Required(ErrorMessage = "Points are required")]
        public int? Points { get; set; }

    }
    public class UpdateTeamsViewModel : CreateTeamsViewModel
    {
        public int Id { get; set; }
    }
}
