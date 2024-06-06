using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class ArticleGalleryViewModel
    {
        public int Id { get; set; }
        public int? ArticleId { get; set; }
        public string? ImgBase64 { get; set; }
    }

    public class CreateArticleGalleryViewModel
    {
        public int? ArticleId { get; set; }
        public string? ImgBase64 { get; set; }
    }

    public class UpdateArticleGalleryViewModel : CreateArticleGalleryViewModel {
        public int Id { get; set; }
    }
}
