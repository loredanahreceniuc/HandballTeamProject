using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
    }
}
