using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid.Template.Extensions;

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

            var query = _forecasts;
            var likeOptions = new LikeOptions(PatternStyle.VisualBasic);

            var filteredColumns = e.Columns.Where(f => !string.IsNullOrEmpty(f.SearchValue?.ToString())).ToArray();
            foreach (var column in filteredColumns)
            {
                var search = column.SearchValue?.ToString();
                query = (column.Field) switch
                {
                    nameof(WeatherForecast.Date) => query.Where(f => f.Date.ToShortDateString().Like(search, likeOptions)),
                    nameof(WeatherForecast.TemperatureC) => query.Where(f => f.TemperatureC.ToString().Like(search, likeOptions)),
                    nameof(WeatherForecast.TemperatureF) => query.Where(f => f.TemperatureF.ToString().Like(search, likeOptions)),
                    nameof(WeatherForecast.Summary) => query.Where(f => f.Summary.Like(search, likeOptions)),
                    _ => query
                };
            }

            var sortedColumns = e.Columns.Where(f => f.SortIndex >= 0).OrderByDescending(f => f.SortIndex).ToArray();
            foreach (var column in sortedColumns)
            {
                query = (column.Field, column.SortDirection) switch
                {
                    (nameof(WeatherForecast.Date), SortDirection.Ascending) => query.OrderBy(f => f.Date),
                    (nameof(WeatherForecast.Date), SortDirection.Descending) => query.OrderByDescending(f => f.Date),
                    (nameof(WeatherForecast.TemperatureC), SortDirection.Ascending) => query.OrderBy(f => f.TemperatureC),
                    (nameof(WeatherForecast.TemperatureC), SortDirection.Descending) => query.OrderByDescending(f => f.TemperatureC),
                    (nameof(WeatherForecast.TemperatureF), SortDirection.Ascending) => query.OrderBy(f => f.TemperatureF),
                    (nameof(WeatherForecast.TemperatureF), SortDirection.Descending) => query.OrderByDescending(f => f.TemperatureF),
                    (nameof(WeatherForecast.Summary), SortDirection.Ascending) => query.OrderBy(f => f.Summary),
                    (nameof(WeatherForecast.Summary), SortDirection.Descending) => query.OrderByDescending(f => f.Summary),
                    _ => query
                };
            }

            TotalItems = query.Count();
            Data = query.Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToArray();
        }

        private IQueryable<WeatherForecast> _forecasts;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 25).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
