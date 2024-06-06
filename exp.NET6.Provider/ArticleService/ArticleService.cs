using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.Articles;
using exp.NET6.Models.ViewModel;
using exp.NET6.Services.DBServices.ArticleService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IGenericService _genericService;

        public ArticleService(IArticleRepository articleRepository, IGenericService genericService)
        {
            _articleRepository = articleRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<ArticleViewModel>> GetAllArticle(string? searchQuery, int pageNumber, int pageSize)
        {
            var pagination = new Pagination<ArticleViewModel>();
            var article = _articleRepository.GetAllQuerable().Where(x=> x.IsDeleted == false);

            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                article = article.Where(x=> x.Title.Contains(searchQuery) || x.ShortDescription.Contains(searchQuery));
            }

            var itemCount = await article.CountAsync();
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await article.Skip(pageSize *(pageNumber - 1)).Take(pageSize).Select(x => new ArticleViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl),
                ShortDescription = x.ShortDescription,
                CategoryId = x.CategoryId,
                Tags=x.Tags,
                Date=x.Date,
            }).ToListAsync();

            return pagination;
        }

        public async Task<EditArticleViewModel> GetArticle(int id)
        {
            var article = await _articleRepository.Get(id);

            if (article == null)
            {
                throw new KeyNotFoundException("This article does not exist!");
            }
            var returnArticle = new EditArticleViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ImgBase64 = _genericService.GetImageFormat(article.ImgUrl),
                ShortDescription = article.ShortDescription,
                CategoryId = article.CategoryId,
                Tags=article.Tags,
                Date=article.Date,
                Draft = article.Draft,
            };
            return returnArticle;
        }

        public async Task CreateArticle(CreateArticleViewModel createArticle)
        {
            var addArticle = new Article()
            {
                Title = createArticle.Title,
                Content = createArticle.Content,
                ImgUrl = _genericService.GetImagePath(createArticle.ImgBase64, null, "article"),
                ShortDescription =createArticle.ShortDescription,
                CategoryId = createArticle.CategoryId,
                CreatedBy=createArticle.CreatedBy,
                AprovedBy=createArticle.AprovedBy,
                Tags=createArticle.Tags,
                Date = DateTime.Now,
            };
            await _articleRepository.Add(addArticle);
        }

        public async Task UpdateArticle(int id, UpdateArticleViewModel updateArticle)
        {
            var article = await _articleRepository.Get(id);
            if (article == null)
            {
                throw new KeyNotFoundException("This article does not exist");
            }

            article.Title = updateArticle.Title;
            article.Content = updateArticle.Content;
            article.ImgUrl = _genericService.GetImagePath(updateArticle.ImgBase64, article.ImgUrl, "article");
            article.ShortDescription = updateArticle.ShortDescription;
            article.CategoryId = updateArticle.CategoryId;
            article.Tags = updateArticle.Tags;
            
            await _articleRepository.Update(article);
        }

        public async Task DeleteArticlePhysical(int id)
        {
            var article = await _articleRepository.Get(id);
            if (article == null)
            {
                throw new KeyNotFoundException("This article does not exist");
            }

            await _articleRepository.DeleteVirtual(id);
        }

        public async Task DeleteArticleVirtual(int id)
        {
            var article = await _articleRepository.Get(id);

            if (article == null)
            {
                throw new KeyNotFoundException("This article does not exist");
            }

            article.IsDeleted = true;

            await _articleRepository.Update(article);

            //await _articleRepository.DeleteVirtual(id);
        }
    }

}

