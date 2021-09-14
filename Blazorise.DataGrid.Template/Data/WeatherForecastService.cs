using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazorise.DataGrid.Template.Data
{
    public class WeatherForecastService
    {
        public WeatherForecast[] Data { get; set; }      

        public int TotalItems { get; set; }

        public async Task OnReadDataAsync(DataGridReadDataEventArgs<WeatherForecast> e)
        {
            if (_forecasts == null)
            {
                var items = await GetForecastAsync(DateTime.Now);
                _forecasts = items.AsQueryable<WeatherForecast>();
            }
       
            TotalItems = _forecasts.Count();     
            Data = _forecasts.Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToArray();
        }      

        private IQueryable<WeatherForecast> _forecasts; 

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }  
    }
}
