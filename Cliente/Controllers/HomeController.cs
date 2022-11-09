﻿using Cliente.Models;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
    }
}