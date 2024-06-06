using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class TeamsMatch
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public int? MatchId { get; set; }

        public virtual Match? Match { get; set; }
        public virtual Team? Team { get; set; }
    }
}
