using DogRallyMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace DogRallyMVC.Services
{
    public class PostTrackToAPI : IPostTrackToAPI
    {
        public Task<HttpContent> PostTrack(TrackExerciseViewModelDTO tevm)
        {
            try
            {
                var json = JsonConvert.SerializeObject(tevm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return Task.FromResult(content as HttpContent);
            }
            catch (Exception ex)
            {
                // Log error or handle it as needed
                throw new InvalidOperationException("Error serializing the track data.", ex);
            }
        }

    }
}
