using CandidateUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CandidateUI.Controllers
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
        public IActionResult CandidateTable()
        {
            return View();
        } 
        public IActionResult AddCandidate()
        {
            return View();
        }  
        public IActionResult Login()
        {
            return View();
        } 
        public IActionResult Register()
        {
            return View();
        } 
        public IActionResult BookManagement()
        {
            return View();
        }
          public IActionResult AddBook()
        {
            return View();
        }  
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult UserDashBoard()
        {
            return View();
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
