using System;
using System.Net;
using System.Text;
using ReadyTech.BrewCoffee2023New.API.Models.Response;
using ReadyTech.BrewCoffee2023New.API.Properties;

namespace ReadyTech.BrewCoffee2023New.API.Common
{
	public class OpenWeatherHelper
	{
		private OpenWeatherAPI _openWeatherAPI { get; set; }
		public OpenWeatherHelper(OpenWeatherAPI openWeatherAPI) {
			_openWeatherAPI = openWeatherAPI;

        }

        public async Task<OpenWeatherResult> GetCurrentCityWeather()
		{
			try
			{
                var client = new HttpClient();
                var endpoint = new Uri(string.Format(_openWeatherAPI.BaseUrl,_openWeatherAPI.CurrentCity,_openWeatherAPI.Key));
                var response = await client.GetAsync(endpoint);
                var responseString = await response.Content.ReadAsStringAsync();

				return System.Text.Json.JsonSerializer.Deserialize<OpenWeatherResult>(responseString);
            }
			catch (Exception ex)
			{
				throw;
			}


		}
	}
}

