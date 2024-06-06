using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.ArticleGallerys
{
    public interface IArticleGalleryService
    {
        Task<Pagination<ArticleGalleryViewModel>> GetAllArticleGallery(int pageNumber, int pageSize);
        Task<ArticleGalleryViewModel> GetArticleGallery(int id);
        Task CreateArticleGallery(CreateArticleGalleryViewModel createArticleGallery);
        Task UpdateArticleGallery(int id, UpdateArticleGalleryViewModel updateArticleGallery);
        Task DeleteArticleGalleryPhysical(int id);
        Task DeleteArticleGalleryVirtual(int id);
    }
}
