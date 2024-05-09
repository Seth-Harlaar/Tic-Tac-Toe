using Game2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Game2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        // have to set default for id otherwise if there is none router freaks out
        public IActionResult Privacy(int id = 0)
        {
            // initialize a model and populate data
            var gameModel = new TicTacModel(id);

            return View(gameModel);
        }


        public JsonResult UpdateGame(int id, int loc, int symbol)
        {
            int[] gameIdArray = id.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();
            gameIdArray[loc] = symbol;

            int newId = 0;
            foreach (int num in gameIdArray)
            {
                newId = 10 * newId + num;
            }

            return Json(new {id = newId});
        }


        public JsonResult ComputerTurn(int id)
        {
            // get game array, find locations to play in, randomly chose one and apply the move
            int[] gameIdArray = id.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

            List<int> moves = new List<int>();

            for(int i = 0; i < gameIdArray.Length; i++)
            {
                if (gameIdArray[i] == 1)
                {
                    moves.Add(i);
                }
            }

            
            Random rnd = new Random();
            int nextMove = rnd.Next(moves.Count);

            gameIdArray[moves[nextMove]] = 3;

            // convert back to int
            int newId = 0;
            foreach (int num in gameIdArray)
            {
                newId = 10 * newId + num;
            }

            return Json(new { id = newId });
        }



        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
