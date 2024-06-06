using System;
using System.Collections.Generic;

namespace exp.NET6.Infrastructure.Entities
{
    public partial class staff
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Role { get; set; }
        public string? ImgUrl { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
