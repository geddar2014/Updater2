using System.Collections.Generic;
using Newtonsoft.Json;

namespace Updater.Apis.Dtos
{
    public class GetSeasonsOutput
    {
        [JsonProperty("statistic")]
        public SOutStats OutStats { get; set; }

        public sealed class SOutStats
        {
            [JsonProperty("N")]
            public IList<SeasonDto> Seasons { get; set; }
        }
    }
}