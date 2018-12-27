using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Client.ViewModels
{
    public class AddVideoViewModel
    {
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        [Required]
        public string Title { get; set; }
    }
}
