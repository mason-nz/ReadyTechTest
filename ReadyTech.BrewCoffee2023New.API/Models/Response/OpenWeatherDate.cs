using System;
using System.Text.Json.Serialization;
using ReadyTech.BrewCoffee2023New.API.Common;

namespace ReadyTech.BrewCoffee2023New.API.Models.Response
{
	public class OpenWeatherDate
	{
        [JsonPropertyName("dt")]
        public int Dt { set; get; }

        [JsonPropertyName("main")]
        public OpenWeatherMain openWeatherMain { get; set; }

        public DateTime DtDate { get { return UTCCoverter.ConvertIntDatetime(Dt); } }
    }
}

