using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DogRallyMVC.Models
{
    public class ExerciseDTO
    {
        // Serialization
        // Validation: Data Annotation
        // Declaration of public properties

        [JsonPropertyName ("exerciseID")]
        [Required]
        [Range (1, int.MaxValue, ErrorMessage = "Exercise ID must be greater than 0")]
        public int ExerciseID { get; set; }

        [JsonPropertyName("exerciseIllustrationPath")]
        [Required]
        public string ExerciseIllustrationPath { get; set; }

        [JsonPropertyName ("exercisePositionX")]
        [Required]
        public double PositionX { get; set; }

        [JsonPropertyName("exercisePositionY")]
        [Required]
        public double PositionY { get; set; }
    }
}
