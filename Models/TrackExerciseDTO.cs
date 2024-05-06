using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DogRallyMVC.Models
{
    public class TrackExerciseDTO
    {
        // Join table
        // Declaration of public properties

        // Foreign keys
        [JsonPropertyName("execiseID")]
        [Required]
        public int ExerciseID { get; set; }

        [JsonPropertyName("trackID")]
        [Required]
        public int TrackID {  get; set; }

        [JsonPropertyName("trackName")]
        [Required]
        public string TrackName { get; set; }

        // Payload
        [JsonPropertyName("trackExercisePositionX")]
        [Required]
        public double TrackExercisePositionX { get; set; }

        [JsonPropertyName("trackExercisePositionY")]
        [Required]
        public double TrackExercisePositionY { get; set; }
    }
}
