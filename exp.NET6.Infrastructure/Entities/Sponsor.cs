using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Sponsor
    {
        public int Id { get; set; }
        public string? ImgUrl { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public bool? IsDeleted { get; set; }
    }
}
