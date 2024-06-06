using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class TeamsRanking
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public string Goals { get; set; } = null!;
        public int Points { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
