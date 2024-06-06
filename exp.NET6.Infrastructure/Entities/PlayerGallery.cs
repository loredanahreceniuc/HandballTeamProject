using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class PlayerGallery
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string ImgUrl { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual Player Player { get; set; } = null!;
    }
}
