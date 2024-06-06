using exp.NET6.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.ArticleService
{
    public interface IArticleService
    {
        Task<Pagination<ArticleViewModel>> GetAllArticle(string? searchQuery, int pageNumber, int pageSize);
        Task<EditArticleViewModel> GetArticle(int id);
        Task CreateArticle(CreateArticleViewModel createArticle);
        Task UpdateArticle(int id, UpdateArticleViewModel updateArticle);
        Task DeleteArticlePhysical(int id);
        Task DeleteArticleVirtual(int id);
    }
}
