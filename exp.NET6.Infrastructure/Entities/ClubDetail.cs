using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class ClubDetail
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
