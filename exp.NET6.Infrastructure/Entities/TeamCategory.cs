using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class TeamCategory
    {
        public TeamCategory()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
