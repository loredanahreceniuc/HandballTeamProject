using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class PlayerHistoryViewModel
    {
        public int Id { get; set; }
        public string Team { get; set; } = null!;
        public string Season { get; set; } = null!;
        public string Results { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int? PlayerId { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class CreatePlayerHistoryViewModel
    {
        [Required(ErrorMessage = "Team is required")]
        [MaxLength(100)]
        public string Team { get; set; } = null!;
        public string Season { get; set; } = null!;
        [Required(ErrorMessage = "Result is required")]
        public string Results { get; set; } = null!;
        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; } = null!;
        public int? PlayerId { get; set; }
        public bool? IsDeleted { get; set; }


    }
    public class UpdatePlayerHistoryViewModel : CreatePlayerHistoryViewModel {
        public int Id { get; set; }

    }
}
