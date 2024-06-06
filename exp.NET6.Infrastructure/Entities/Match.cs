using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Home { get; set; } = null!;
        public string Away { get; set; } = null!;
        public int? HomePoints { get; set; }
        public int? AwayPoints { get; set; }
        public int? CompetitionId { get; set; }
        public string? HomeImgUrl { get; set; }
        public string? AwayImgUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Competition? Competition { get; set; }
    }
}
