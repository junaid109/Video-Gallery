using ImageGallery.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VideoGallery.Client.Services;
using VideoGallery.Model;

namespace VideoGallery.Client.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoGalleryHttpClient _videoGalleryHttpClient;

        public VideoController(IVideoGalleryHttpClient videoGalleryHttpClient)
        {
            _videoGalleryHttpClient = videoGalleryHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            // call the API
            var httpClient = await _videoGalleryHttpClient.GetClient();

            var response = await httpClient.GetAsync("api/videos").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var videosAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var galleryIndexViewModel = new GalleryIndexViewModel(
                    JsonConvert.DeserializeObject<IList<Video>>(videosAsString).ToList());

                return View(galleryIndexViewModel);
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        public async Task<IActionResult> EditVideo(Guid id)
        {
            // call the API
            var httpClient = await _videoGalleryHttpClient.GetClient();

            var response = await httpClient.GetAsync($"api/videos/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var videoAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var deserializedVideo = JsonConvert.DeserializeObject<Video>(videoAsString);

                var editVideoViewModel = new EditVideoViewModel()
                {
                    Id = deserializedVideo.Id,
                    Title = deserializedVideo.Title
                };

                return View(editVideoViewModel);
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVideo(EditVideoViewModel editVideoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // create an VideoForUpdate instance
            var videoForUpdate = new VideoForUpdate()
            { Title = editVideoViewModel.Title };

            // serialize it
            var serializedVideoForUpdate = JsonConvert.SerializeObject(videoForUpdate);

            // call the API
            var httpClient = await _videoGalleryHttpClient.GetClient();

            var response = await httpClient.PutAsync(
                $"api/videos/{editVideoViewModel.Id}",
                new StringContent(serializedVideoForUpdate, System.Text.Encoding.Unicode, "application/json"))
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        public async Task<IActionResult> DeleteVideo(Guid id)
        {
            // call the API
            var httpClient = await _videoGalleryHttpClient.GetClient();

            var response = await httpClient.DeleteAsync($"api/videos/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        public IActionResult AddVideo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVideo(AddVideoViewModel addVideoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // create an VideoForCreation instance
            VideoForCreation videoForCreation = new VideoForCreation
            { Title = addVideoViewModel.Title };

            // take the first (only) file in the Files list
            var VideoFile = addVideoViewModel.Files.First();

            if (VideoFile.Length > 0)
            {
                using (var fileStream = VideoFile.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    videoForCreation.Bytes = ms.ToArray();
                }
            }

            // serialize it
            var serializedVideoForCreation = JsonConvert.SerializeObject(videoForCreation);

            // call the API
            var httpClient = await _videoGalleryHttpClient.GetClient();

            var response = await httpClient.PostAsync(
                $"api/Videos",
                new StringContent(serializedVideoForCreation, System.Text.Encoding.Unicode, "application/json"))
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }
    }
}
