using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class Article
    {
        public Article()
        {
            ArticleGalleries = new HashSet<ArticleGallery>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImgUrl { get; set; }
        public string? ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public string? CreatedBy { get; set; }
        public string? AprovedBy { get; set; }
        public string? Tags { get; set; }
        public DateTime Date { get; set; }
        public bool Draft { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual AspNetUser? AprovedByNavigation { get; set; }
        public virtual BlogCategory? Category { get; set; }
        public virtual AspNetUser? CreatedByNavigation { get; set; }
        public virtual ICollection<ArticleGallery> ArticleGalleries { get; set; }
    }
}
