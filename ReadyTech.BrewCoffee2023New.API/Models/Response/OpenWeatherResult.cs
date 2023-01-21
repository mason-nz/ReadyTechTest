using System;
using System.Text.Json.Serialization;

namespace ReadyTech.BrewCoffee2023New.API.Models.Response
{
	public class OpenWeatherResult
	{
		[JsonPropertyName("cod")]
		public string Cod { get; set; }

        [JsonPropertyName("message")]
        public int Message { get; set; }

        [JsonPropertyName("cnt")]
        public int Cnt { get; set; }

        [JsonPropertyName("list")]
        public List<OpenWeatherDate> List { get; set; }
	}
}

