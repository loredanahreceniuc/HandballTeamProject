using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Trophy
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
