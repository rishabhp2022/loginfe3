using loginfe3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace loginfe3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        Uri baseAddress = new Uri("http://localhost:5176/api");

        private readonly HttpClient _client;
        
        public HomeController()
        {
            //ssl crtfct
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            //new added code

            _client = new HttpClient(clientHandler);
            _client.BaseAddress = baseAddress;
        }

        

        public IActionResult Index()
        {
            return View();
        }
       
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        //Registration method
        [HttpPost]
        public IActionResult Create(UserRegistration model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Registration/PostUser", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "User Successfully Registered";
                return RedirectToAction("AfterRegistrationPage");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            // Call your login API using HttpClient
            var apiUrl = "http://localhost:5176/api";
            var Login = new { UserName = userName, Password = password };
            var response = await _client.PostAsJsonAsync(apiUrl, Login);
            if (response.IsSuccessStatusCode)
            {
                // Successful login, redirect to user home page
                return View("Dashboard");
            }
            else
            {
                // Failed login, handle accordingly (e.g., show error message)
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View();
            }
        }



        public IActionResult Dashboard()
        {
            return View();
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
    }
}