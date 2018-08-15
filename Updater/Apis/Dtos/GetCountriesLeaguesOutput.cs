using System.Collections.Generic;
using Newtonsoft.Json;

namespace Updater.Apis.Dtos
{
    public class GetCountriesLeaguesOutput
    {
        [JsonProperty("statistic")]
        public CLOutStats OutStats { get; set; }

        public sealed class CLOutStats
        {
            [JsonProperty("S")]
            public IList<CountryDto> Countries { get; set; }
        }
    }
}