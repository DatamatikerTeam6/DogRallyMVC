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
        private readonly IHttpClientFactory _httpClientFactory;

        public TracksController(ILogger<TracksController> logger, IPostTrackToAPI postTrackToAPI, IHttpClientFactory httpClientFactory, IGetExercisesFromAPI getExercisesFromAPI)
        {
            _logger = logger;
            _postTrackToAPI = postTrackToAPI;
            _httpClientFactory = httpClientFactory;
            _getExercisesFromAPI = getExercisesFromAPI;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CreateTrack()
        {
            var client = _httpClientFactory.CreateClient();

            Console.WriteLine("Før kald til GetExercises");

            var exercises = await _getExercisesFromAPI.GetExercises(client);

            Console.WriteLine("Efter kald til GetExercises");

            var viewModel = new TrackExerciseViewModelDTO
            {
                Track = new TrackDTO(),
                Exercises = exercises
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrack(TrackExerciseViewModelDTO tevm)
        {
            var client = _httpClientFactory.CreateClient();
            
            var content = await _postTrackToAPI.PostTrack(tevm);

            var response = await client.PostAsync("https://localhost:7183/Tracks/CreateTrack", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Success");
            }
           
            else
            {
                return RedirectToAction("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
