using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private DataContext _dataContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            var data = await _dataContext.weatherForecasts.ToArrayAsync();
            return this.Ok(data);
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> Get(int id)
        {            
            var data = await _dataContext.weatherForecasts.Where(x => x.Id == id).FirstOrDefaultAsync();
            return this.Ok(data);
        }
    }
}
