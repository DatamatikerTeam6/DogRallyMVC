using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DogRallyMVC.Models
{
    public class TrackDTO
    {
        // Serialization
        // Validation: Data Annotation
        // Declaration of public properties

        [JsonPropertyName("trackID")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Track ID must be greater than 0")]
        public int TrackID { get; set; }

        [JsonPropertyName("trackName")]
        [Required]
        public string TrackName { get; set; }

        [JsonPropertyName("trackDate")]
        [Required]
        public DateTime TrackDate { get; set; }
    }
}
