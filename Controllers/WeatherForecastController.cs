using EmpSubbieWebAPI.Data.Repositories;
using EmpSubbieWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserFormsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserFormsController> _logger;
        private readonly IUserService _userService;
        private readonly IDataRepository _dataRepo;

        public UserFormsController(ILogger<UserFormsController> logger, IUserService userService, IDataRepository datarepo)
        {
            _logger = logger;
            _userService = userService;
            _dataRepo = datarepo;
        }


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

       
        [HttpGet("test")]
        public async Task<IActionResult> IsAuthorisedAsync()
        {

            var test = await _userService.AuthenticateUserAsync("email here", "****");

            var data = await _dataRepo.GetFormsForUserAsync("cc3555b8-f969-4cc4-9692-0f7971504380");

            var form = await _dataRepo.GetFormForUserAsync("cc3555b8-f969-4cc4-9692-0f7971504380", 1);

            return Ok(form);
        }
    }
}
