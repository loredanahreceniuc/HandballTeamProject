using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } =null!;
        public string? Description { get; set; }
        public List<string>? Contact{ get; set; }
        public List<Program>? Program { get; set; }
    }
    
    public class Program
    {
        public string? Day { get; set; }
        public string? Schedule { get; set; }
    }
}
