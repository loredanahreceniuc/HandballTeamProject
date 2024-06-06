using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class PlayerGalleryViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string? ImgBase64 { get; set; }
    }

    public class GalleryViewModel
    {
        public int Id { get; set; }
        public string? ImgBase64 { get; set; }
    }
    public class CreatePlayerGalleryViewModel
    {
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public string? ImgBase64 { get; set; }

    }
    public class UpdatePlayerGalleryViewModel : CreatePlayerGalleryViewModel
    {
        public int Id { get; set; }
    }
}
