using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.ArticleGallerys;
using exp.NET6.Models.ViewModel;

using MailKit.Search;

using Microsoft.EntityFrameworkCore;

using Org.BouncyCastle.Asn1.Mozilla;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.ArticleGallerys
{
    public class ArticleGalleryService:IArticleGalleryService
    {
        private readonly IArticleGalleryRepository _articleGalleryRepository;
        private readonly IGenericService _genericService;

        public ArticleGalleryService(IArticleGalleryRepository articleGalleryRepository, IGenericService genericService)
        {
            _articleGalleryRepository = articleGalleryRepository;
            _genericService = genericService;
        }

        public async Task<Pagination<ArticleGalleryViewModel>> GetAllArticleGallery(int pageSize, int pageNumber)
        {
            var pagination = new Pagination<ArticleGalleryViewModel>();
            var articleGallery = _articleGalleryRepository.GetAllQuerable();
            var itemCount = await articleGallery.CountAsync();

            
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await articleGallery.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new ArticleGalleryViewModel()
            {
                Id = x.Id,
                ArticleId = x.ArticleId,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl)                
            }).ToListAsync();
            return pagination;
        }

        public async Task<ArticleGalleryViewModel> GetArticleGallery(int id)
        {
            var articleGallery = await _articleGalleryRepository.Get(id);

            if(articleGallery == null)
            {
                throw new KeyNotFoundException("This article does not exist. ");
            }

            var returnArticleGallery = new ArticleGalleryViewModel()
            {
                Id = articleGallery.Id,
                ArticleId = articleGallery.ArticleId,
                ImgBase64 =_genericService.GetImageFormat(articleGallery.ImgUrl),
            };

            return returnArticleGallery;
        }

        public async Task CreateArticleGallery(CreateArticleGalleryViewModel createArticleGallery)
        {
            var addArticleGallery = new ArticleGallery()
            {
                ArticleId = createArticleGallery.ArticleId,
                ImgUrl = _genericService.GetImagePath(createArticleGallery.ImgBase64, null, "articleGallery"),
            };
            await _articleGalleryRepository.Add(addArticleGallery);
        }
        
        public async Task UpdateArticleGallery(int id, UpdateArticleGalleryViewModel updateArticleGallery)
        {
            var articleGallery = await _articleGalleryRepository.Get(id);
            if(articleGallery != null)
            {
                throw new KeyNotFoundException("This article does not exist");
            }
            articleGallery.ImgUrl= _genericService.GetImagePath(updateArticleGallery.ImgBase64, articleGallery.ImgUrl, "articleGallery");
            await _articleGalleryRepository.Update(articleGallery);
        }

        public async Task DeleteArticleGalleryPhysical(int id)
        {
            var articleGallery = await _articleGalleryRepository.Get(id);
            if (articleGallery == null)
            {
                throw new KeyNotFoundException("This article does not exist");
            }

            await _articleGalleryRepository.DeleteVirtual(id);
        }

        public async Task DeleteArticleGalleryVirtual(int id)
        {
            var articleGallery = await _articleGalleryRepository.Get(id);

            if (articleGallery == null)
            {
                throw new KeyNotFoundException("This article does not exist");
            }
            await _articleGalleryRepository.DeleteVirtual(id);
        }
    }
}
