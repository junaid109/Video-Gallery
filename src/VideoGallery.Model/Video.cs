using System;

namespace VideoGallery.Model
{
    public class Video
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }
        
        public string Description { get; set; }

        public int Likes { get; set; }

        public int Shares { get; set; }
    }
}

