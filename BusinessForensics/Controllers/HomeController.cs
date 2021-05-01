using BusinessForensics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using BusinessForensics.Services;

namespace BusinessForensics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _gameService;

        public HomeController(ILogger<HomeController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Created(Attempt attemptModel)
        {
            attemptModel.GameAttempt = _gameService.GetAttemptByGame(attemptModel.GameId);
            return View("Index", attemptModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var game = await _gameService.CreateNewGame();
            var attemptModel = new Attempt
            {
                GameId = game.Id,
                GameAttempt = game.GameAttempts,
                AttemptCountLeft = 3
            };
            return RedirectToAction("Created", attemptModel);
        }

        [HttpPost]
        public async Task<IActionResult> Send(Attempt attempt)
        {
            attempt.AttemptCountLeft = attempt.AttemptCountLeft - 1;
            var result = await _gameService.AddAttempt(attempt.GameId, attempt.AttemptNumber);
            attempt.Message = result ? "You correctly guessed the number" : "Sorry it's not the correct number";
            return RedirectToAction("Created", attempt);
        }
    }
}
