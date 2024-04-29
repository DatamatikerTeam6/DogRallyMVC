using DogRallyMVC.Models;

namespace DogRallyMVC.Services
{
    public interface IPostTrackToAPI
    {
        Task<HttpContent> PostTrack(TrackExerciseViewModelDTO tevm);
    }
}