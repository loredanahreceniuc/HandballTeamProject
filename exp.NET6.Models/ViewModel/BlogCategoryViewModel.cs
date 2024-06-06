using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class BlogCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class CreateBlogCategoryViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }
    }
    public class UpdateBlogCategoryViewModel :CreateBlogCategoryViewModel
    {
        public int Id {get; set;}
       
    }
}
