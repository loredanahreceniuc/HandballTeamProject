using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string MatchDate { get; set; }
        public bool IsHome { get; set; } 
        public string Home { get; set; } = null!;
        public string Away { get; set; } = null!;
        public int? HomePoints { get; set; }
        public int? AwayPoints { get; set; }
        public MathCompetition? CompetitionId { get; set; }
        public string? HomeImgBase64 { get; set; }
        public string? AwayImgBase64 { get; set; }
    }

    public class MathCompetition
    {
        public int? Id { get; set; }
        public string? Name { get; set; } 
    }

    public class CreateMatchViewModel
    {
        [Required(ErrorMessage ="Match date is required")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage ="Home team is required")]
        [MaxLength(300)]
        public string Home { get; set; } = null!;
        [Required(ErrorMessage ="Away team is required")]
        [MaxLength(300)]
        public string Away { get; set; } = null!;
        public int? HomePoints { get; set; }
        public int? AwayPoints { get; set; }
        public int? CompetitionId { get; set; }
        public string? HomeImgBase64 { get; set; }
        public string? AwayImgBase64 { get; set; }

    }

    public class UpdateMatchViewModel: CreateMatchViewModel 
    { }
}
