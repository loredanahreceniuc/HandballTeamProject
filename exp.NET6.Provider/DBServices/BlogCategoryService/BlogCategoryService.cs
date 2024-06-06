using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.BlogCategories;
using exp.NET6.Models.ViewModel;

using MailKit.Search;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.BlogCategories
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IGenericService _genericService;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository, IGenericService genericService)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<BlogCategoryViewModel>> GetAllBlogCategory(string? searchQuery, int pageSize, int pageNumber) 
        {
            var pagination = new Pagination<BlogCategoryViewModel>();
            var blogCategory = _blogCategoryRepository.GetAllQuerable();

            var itemCount = await blogCategory.CountAsync();
            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                blogCategory = blogCategory.Where(x => x.Name.Contains(searchQuery));
            }
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await blogCategory.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new BlogCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return pagination;
        }

        public async Task<BlogCategoryViewModel> GetBlogCategory(int id)
        {
            var blogCategory = await _blogCategoryRepository.Get(id);

            if (blogCategory == null)
            {
                throw new KeyNotFoundException("This blog category does not exist. ");
            }

            var returnBlogCategory = new BlogCategoryViewModel()
            {
                Id = blogCategory.Id,
                Name = blogCategory.Name,
            };

            return returnBlogCategory;
        }

        public async Task CreateBlogCategory(CreateBlogCategoryViewModel createBlogCategory)
        {
            var addBlogCategory = new BlogCategory()
            {
                Name = createBlogCategory.Name,

            };
            await _blogCategoryRepository.Add(addBlogCategory);
        }

        public async Task UpdateBlogCategory(int id, UpdateBlogCategoryViewModel updateBlogCategory)
        {
            var blogCategory = await _blogCategoryRepository.Get(id);
            if (blogCategory != null)
            {
                throw new KeyNotFoundException("This blog category does not exist");
            }
            await _blogCategoryRepository.Update(blogCategory);
        }

        public async Task DeleteBlogCategoryPhysical(int id)
        {
            var blogCategory = await _blogCategoryRepository.Get(id);
            if (blogCategory == null)
            {
                throw new KeyNotFoundException("This blog category does not exist");
            }

            await _blogCategoryRepository.DeleteVirtual(id);
        }

        public async Task DeleteBlogCategoryVirtual(int id)
        {
            var blogCategory = await _blogCategoryRepository.Get(id);

            if (blogCategory == null)
            {
                throw new KeyNotFoundException("This blog category does not exist");
            }
            await _blogCategoryRepository.DeleteVirtual(id);
        }

    }
}
