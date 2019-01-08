using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VideoGallery.API.Services;
using System;
using VideoGallery.Model;
using System.IO;

namespace VideoGallery.API.Controllers
{
    [Route("api/videos")]
    [ApiController]
    public class VideosController : Controller
    {
        private readonly IVideoGalleryRepository _videoGalleryRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public VideosController(IVideoGalleryRepository videoGalleryRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _videoGalleryRepository = videoGalleryRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet()]
        public IActionResult GetVideos()
        {
            // get from repo
            var videosFromRepo = _videoGalleryRepository.GetVideos();

            // map to model
            var videosToReturn = Mapper.Map<IEnumerable<Model.Video>>(videosFromRepo);

            // return
            return Ok(videosToReturn);
        }


        [HttpGet("{id}", Name = "GetVideo")]
        public IActionResult GetVideo(Guid id)
        {
            var videoFromRepo = _videoGalleryRepository.GetVideo(id);

            if (videoFromRepo == null)
            {
                return NotFound();
            }

            var videoToReturn = Mapper.Map<Model.Video>(videoFromRepo);

            return Ok(videoToReturn);
        }

        [HttpPost()]
        public IActionResult CreateVideo([FromBody] VideoForCreation videoForCreation)
        {
            if (videoForCreation == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 - Unprocessable Entity when validation fails
                return new UnprocessableEntityObjectResult(ModelState);
            }

            // Automapper maps only the Title in our configuration
            var videoEntity = Mapper.Map<Entities.Video>(videoForCreation);

            // Create an image from the passed-in bytes (Base64), and 
            // set the filename on the image

            // get this environment's web root path (the path
            // from which static content, like an image, is served)
            var webRootPath = _hostingEnvironment.WebRootPath;

            // create the filename
            string fileName = Guid.NewGuid().ToString() + ".jpg";

            // the full file path
            var filePath = Path.Combine($"{webRootPath}/images/{fileName}");

            // write bytes and auto-close stream
            System.IO.File.WriteAllBytes(filePath, videoForCreation.Bytes);

            // fill out the filename
            videoEntity.FileName = fileName;

            // ownerId should be set - can't save image in starter solution, will
            // be fixed during the course
            //imageEntity.OwnerId = ...;

            // add and save.  
            _videoGalleryRepository.AddVideo(videoEntity);

            if (!_videoGalleryRepository.Save())
            {
                throw new Exception($"Adding an image failed on save.");
            }

            var videoToReturn = Mapper.Map<Video>(videoEntity);

            return CreatedAtRoute("GetImage",
                new { id = videoToReturn.Id },
                videoToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideo(Guid id)
        {

            var videoFromRepo = _videoGalleryRepository.GetVideo(id);

            if (videoFromRepo == null)
            {
                return NotFound();
            }

            _videoGalleryRepository.DeleteVideo(videoFromRepo);

            if (!_videoGalleryRepository.Save())
            {
                throw new Exception($"Deleting video with {id} failed on save.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideo(Guid id,
          [FromBody] VideoForUpdate videoForUpdate)
        {

            if (videoForUpdate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // return 422 - Unprocessable Entity when validation fails
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var videoFromRepo = _videoGalleryRepository.GetVideo(id);
            if (videoFromRepo == null)
            {
                return NotFound();
            }

            Mapper.Map(videoForUpdate, videoFromRepo);

            _videoGalleryRepository.UpdateVideo(videoFromRepo);

            if (!_videoGalleryRepository.Save())
            {
                throw new Exception($"Updating video with {id} failed on save.");
            }

            return NoContent();
        }
    }
}
