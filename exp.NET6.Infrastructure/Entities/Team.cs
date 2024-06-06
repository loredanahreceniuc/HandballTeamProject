using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImgUrl { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? Draw { get; set; }
        public int? GoalDifference { get; set; }
        public int? Points { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
