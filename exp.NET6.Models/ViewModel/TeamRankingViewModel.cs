using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class TeamRankingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GamesPlayed { get; set; }
        public int? Wins { get; set; }
        public int? Draws { get; set; }
        public int? Losses { get; set; }
        public string Goals { get; set; }

        public int Points { get; set; }

        public string? ImgBase64 { get; set; }
    }
    public class CreateTeamRankingViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Games played are required")]
        public int GamesPlayed { get; set; }

        [Required(ErrorMessage = "Wins are required")]
        public int Wins { get; set; }
        [Required(ErrorMessage = "Draws are required")]
        public int Draws { get; set; }
        [Required(ErrorMessage = "Losses are required")]
        public int Losses { get; set; }
        [Required(ErrorMessage = "Goals are required")]
        [MaxLength(100)]
        public string Goals { get; set; }

        public int Points { get; set; }

        [Required(ErrorMessage = "Points are required")]
        [MaxLength (100)]

        public string? ImgBase64 { get; set; }
    }
    public class UpdateTeamRankingViewModel : CreateTeamRankingViewModel { }
}
