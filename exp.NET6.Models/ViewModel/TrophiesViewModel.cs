using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class TrophiesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Date { get; set; }
        public string? ImgBase64 { get; set; }
    }

    public class TrophyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Date { get; set; }
        public string? ImgBase64 { get; set; }
    }

    public class CreateTrophiesViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = null;
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
        public string? ImgBase64 { get; set; }
    }
    public class UpdateTrophiesViewModel :CreateTrophiesViewModel
    {
    }
}
