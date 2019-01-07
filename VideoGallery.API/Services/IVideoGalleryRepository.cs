using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGallery.Model;

namespace VideoGallery.API.Services
{
    public interface IVideoGalleryRepository
    {
        IEnumerable<Video> GetVideos();
        bool IsVideoOwner(Guid id, string ownerId);
        Video GetVideo(Guid id);
        bool VideoExists(Guid id);
        void AddVideo(Video video);
        void DeleteVideo(Video video);
        bool Save();
    }
}
