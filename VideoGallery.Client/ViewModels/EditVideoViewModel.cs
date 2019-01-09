using VideoGallery.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace VideoGallery.Client.ViewModels
{
    public class EditVideoViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public Guid Id { get; set; }  
    }
}
