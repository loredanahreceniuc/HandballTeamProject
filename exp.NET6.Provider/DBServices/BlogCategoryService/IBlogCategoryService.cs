using exp.NET6.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.BlogCategories
{
    public interface IBlogCategoryService
    {
        Task<Pagination<BlogCategoryViewModel>> GetAllBlogCategory(string? searchQuery, int pageNumber, int pageSize);
        Task<BlogCategoryViewModel> GetBlogCategory(int id);
        Task CreateBlogCategory(CreateBlogCategoryViewModel createBlogCategory);
        Task UpdateBlogCategory(int id, UpdateBlogCategoryViewModel updateBlogCategory);
        Task DeleteBlogCategoryPhysical(int id);
        Task DeleteBlogCategoryVirtual(int id);
    }
}
