using DogRallyMVC.Models;
using DogRallyMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace DogRallyMVC.Controllers
{
    public class TracksController : Controller
    {
        // Dependency Injection
        private readonly ILogger<TracksController> _logger;
        private readonly IPostTrackToAPI _postTrackToAPI;
        private readonly IGetExercisesFromAPI _getExercisesFromAPI;
        private readonly IGetTrackFromAPI _getTrackFromAPI;
        private readonly IHttpClientFactory _httpClientFactory;

        public TracksController(ILogger<TracksController> logger, IPostTrackToAPI postTrackToAPI, IHttpClientFactory httpClientFactory, IGetExercisesFromAPI getExercisesFromAPI, IGetTrackFromAPI getTrackFromAPI)
        {
            _logger = logger;
            _postTrackToAPI = postTrackToAPI;
            _httpClientFactory = httpClientFactory;
            _getExercisesFromAPI = getExercisesFromAPI;
            _getTrackFromAPI = getTrackFromAPI; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateTrack()
        {
            var client = _httpClientFactory.CreateClient();

            Console.WriteLine("Før kald til GetExercises");

            //Get exercises from API
            var exercises = await _getExercisesFromAPI.GetExercises(client);

            Console.WriteLine("Efter kald til GetExercises");

            Console.WriteLine("Berit");
            var viewModel = new TrackExerciseViewModelDTO
            {
                Track = new TrackDTO(),
                Exercises = exercises
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrack([Bind("Exercises, Track")] TrackExerciseViewModelDTO tevm)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            string json = JsonConvert.SerializeObject(tevm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _logger.LogInformation(json);

            try
            {
                var response = await client.PostAsync("https://localhost:7183/Tracks/CreateTrack", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMessage = "Track created successfully!";
                    return View(tevm);  // You could also redirect to a success page as needed
                }
                else
                {
                    // Read the response body for error details
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"API returned an error: {errorResponse}");
                    ViewBag.ApiResponse = $"API Error: {errorResponse}";  // Storing error response in ViewBag for display
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error sending data to API: {ex.Message}");
                ViewBag.ApiResponse = $"Exception: {ex.Message}";  // Store exception message in ViewBag
            }
            return View(tevm); // Return to the view with errors and ViewBag information
        }

        [HttpGet]
        public async Task<IActionResult> ReadTrack(int id)
        {
            var client = _httpClientFactory.CreateClient();

            //Get Track from API
            var trackExercises = await _getTrackFromAPI.GetTrack(client, id);

            return View(trackExercises);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
