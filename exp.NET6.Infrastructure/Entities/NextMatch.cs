using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class NextMatch
    {
        public DateTime MatchDate { get; set; }
        public string Hosts { get; set; } = null!;
        public string Guests { get; set; } = null!;
        public bool? IsDeleted { get; set; }
    }
}
