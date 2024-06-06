using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class PlayerHistory
    {
        public int Id { get; set; }
        public string Team { get; set; } = null!;
        public string Season { get; set; } = null!;
        public string Results { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int? PlayerId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Player? Player { get; set; }
    }
}
