using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Competition
    {
        public Competition()
        {
            Matches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
