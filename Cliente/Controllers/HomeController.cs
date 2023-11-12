using Cliente.Models;
using Cliente.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIds4.Dto;
using System.Diagnostics;
using System.Text.Json;

namespace Cliente.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger,ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public IActionResult Index()
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

        [Authorize]
        public async Task<IActionResult> Weather()
        {
            using var client = new HttpClient();
            //var token = await tokenService.GetToken("");
            var token = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(token);
            var result = await client.GetAsync("https://localhost:7245/WeatherForecast");
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<List<WeatherForecast>>(data);
                return Ok(response);
            }
            throw new Exception("Error");
        }

        [Authorize]
        public async Task<IActionResult> GetToken()
        {
            var result = await _tokenService.GetToken("weatherApi.read");
            return Ok(result);
        }

        [Authorize]
        public async Task<IActionResult> GetTokenHttpContext()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return Ok(token);
        }
    }
}