using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGallery.API.Entities;

namespace VideoGallery.API.Services
{
    public class VideoGalleryRepositorycs : IVideoGalleryRepository, IDisposable
    {
        VideoGalleryContext _context;

        public VideoGalleryRepositorycs(VideoGalleryContext context)
        {
            _context = context;
        }

        public void AddVideo(Video video)
        {
            _context.Videos.Add(video);
        }

        public void DeleteVideo(Video video)
        {
            _context.Videos.Remove(video);
        }

        public Video GetVideo(Guid id)
        {
            return _context.Videos.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Video> GetVideos()
        {
            return _context.Videos
                .OrderBy(i => i.Title).ToList();
        }

        public bool IsVideoOwner(Guid id, string ownerId)
        {
            return _context.Videos.Any(i => i.Id == id && i.OwnerId == ownerId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool VideoExists(Guid id)
        {
            return _context.Videos.Any(i => i.Id == id);
        }


        public void UpdateVideo(Video video)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            }
        }
    }
}
