using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class PlayersViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string? Description { get; set; }
        public string Position { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImgBase64 { get; set; }
        public string Nationality { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? Number { get; set; }
        public bool? MainTeam { get; set; }
    }

    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public int Height { get; set; }
        public int Weight { get; set; }
        public string? Description { get; set; }
        public string Position { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImgBase64 { get; set; }
        public string Nationality { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? Number { get; set; }
        public bool? MainTeam { get; set; }
        public List<string>? ImgGallery { get; set; }
    }

    public class CreatePlayerViewModel
    {
        [Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage ="Last name is required")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage ="Birth date is required")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage ="Height is required")]
        public int Height { get; set; }
        [Required(ErrorMessage ="Weight is required")]
        public int Weight { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage ="Position is required")]
        public string Position { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImgBase64 { get; set; }
        public string Nationality { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? Number { get; set; }
        public List<string>? ImgGallery { get; set; }
        public bool? MainTeam { get; set; }



    }

    public class UpdatePlayerViewModel : CreatePlayerViewModel {
    }

}
