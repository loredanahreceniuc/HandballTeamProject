using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class NextMatchViewModel
    {
        public DateTime MatchDate { get; set; }
        public string Hosts { get; set; } = null!;
        public string Guests { get; set; } = null!;
    }
    public class CreateNextMatchViewModel
    {
        public DateTime MatchDate { get; set; }
        public string Hosts { get; set; } = null!;
        public string Guests { get; set; } = null!;
        

    }

    public class UpdateNextMatchViewModel : CreateNextMatchViewModel { }

}

