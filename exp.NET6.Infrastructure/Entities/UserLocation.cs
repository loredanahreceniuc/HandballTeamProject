using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class UserLocation
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string UserId { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
    }
}
