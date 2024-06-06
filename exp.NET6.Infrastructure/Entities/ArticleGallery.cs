using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class ArticleGallery
    {
        public int Id { get; set; }
        public int? ArticleId { get; set; }
        public string? ImgUrl { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Article? Article { get; set; }
    }
}
