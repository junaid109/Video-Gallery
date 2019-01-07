using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGallery.Model;

namespace VideoGallery.API.Entities
{
    public class VideoGalleryContext : DbContext
    {
        public VideoGalleryContext(DbContextOptions<VideoGalleryContext> options)
            : base(options)
        {

        }

        public DbSet<Video> Videos { get; set; }
    }
}
