using DogRallyMVC.Models;
using Newtonsoft.Json;

namespace DogRallyMVC.Services
{
    public class GetTrackFromAPI : IGetTrackFromAPI
    {
        public async Task<List<TrackExerciseDTO>> GetTrack(HttpClient client, int trackID)
        {
            var url = $"https://localhost:7183/Tracks/GetTrack";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var exercises = JsonConvert.DeserializeObject<List<TrackExerciseDTO>>(responseBody);
                    return exercises;
                }
                else
                {
                    Console.WriteLine($"Anmodningen mislykkedes med statuskode: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Der opstod en undtagelse: {ex.Message}");
            }
            return null;
        }
    }
}
