using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ImgBase64 { get; set; }
        public string? ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public string? Tags { get; set; }
        public DateTime Date { get; set; }
        public bool Draft { get; set; }
    }


    public class EditArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImgBase64 { get; set; }
        public string? ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public string? Tags { get; set; }
        public DateTime Date { get; set; }
        public bool Draft { get; set; }
    }
    public class CreateArticleViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = null!;
        public string? ImgBase64 { get; set; }
        public string? ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public string? CreatedBy { get; set; }
        public string? AprovedBy { get; set; }
        public string? Tags { get; set; }
        public bool Draft { get; set; }
    }

    public class UpdateArticleViewModel : CreateArticleViewModel
    {

    }

}

