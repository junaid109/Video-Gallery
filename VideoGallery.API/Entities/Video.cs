using System;
using System.ComponentModel.DataAnnotations;

namespace VideoGallery.API.Entities
{
    public class Video
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(150)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public int Shares { get; set; }
    }
}
