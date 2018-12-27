using System.Net.Http;
using System.Threading.Tasks;

namespace VideoGallery.Client.Services
{
    public interface IVideoGalleryHttpClient
    {
        Task<HttpClient> GetClient();
    }
}
