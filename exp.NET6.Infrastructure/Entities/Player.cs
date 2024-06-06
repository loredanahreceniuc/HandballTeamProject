using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Player
    {
        public Player()
        {
            PlayerGalleries = new HashSet<PlayerGallery>();
            PlayerHistories = new HashSet<PlayerHistory>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string? Description { get; set; }
        public string Position { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImgUrl { get; set; }
        public string Nationality { get; set; } = null!;
        public int? CategoryId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Number { get; set; }
        public bool? MainTeam { get; set; }

        public virtual TeamCategory? Category { get; set; }
        public virtual ICollection<PlayerGallery> PlayerGalleries { get; set; }
        public virtual ICollection<PlayerHistory> PlayerHistories { get; set; }
    }
}
