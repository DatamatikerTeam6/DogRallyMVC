using DogRallyMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace DogRallyMVC.Services
{
    public class PostTrackToAPI : IPostTrackToAPI
    {
        public async Task<HttpContent> PostTrack(TrackExerciseViewModelDTO tevm)
        {
            var json = JsonConvert.SerializeObject(tevm);
            var content =  new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }

    }
}
