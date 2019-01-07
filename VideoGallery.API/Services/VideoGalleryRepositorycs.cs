using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGallery.API.Entities;
using VideoGallery.Model;

namespace VideoGallery.API.Services
{
    public class VideoGalleryRepositorycs : IVideoGalleryRepository
    {
        VideoGalleryContext _context;

        public VideoGalleryRepositorycs(VideoGalleryContext context)
        {
            _context = context;
        }

        public void AddVideo(Model.Video video)
        {
            throw new NotImplementedException();
        }

        public void DeleteVideo(Model.Video video)
        {
            throw new NotImplementedException();
        }

        public Model.Video GetVideo(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Video> GetVideos()
        {
            throw new NotImplementedException();
        }

        public bool IsVideoOwner(Guid id, string ownerId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool VideoExists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
