using System;
using System.Text.Json.Serialization;

namespace ReadyTech.BrewCoffee2023New.API.Models.Response
{
	public class OpenWeatherMain
	{
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
	}
}

