using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class BlogCategory
    {
        public BlogCategory()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
