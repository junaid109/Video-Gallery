using VideoGallery.Model;
using System.Collections.Generic;

namespace ImageGallery.Client.ViewModels
{
    public class GalleryIndexViewModel
    {
        public IEnumerable<Video> Videos { get; private set; }
            = new List<Video>();

        public GalleryIndexViewModel(List<Video> videos)
        {
            Videos = videos;
        }
    }
}
