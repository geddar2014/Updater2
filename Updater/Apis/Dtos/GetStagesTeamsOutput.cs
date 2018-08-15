using System.Collections.Generic;
using Newtonsoft.Json;

namespace Updater.Apis.Dtos
{
    public class GetStagesTeamsOutput
    {
        [JsonProperty("statistic")]
        public STOutStats OutStats { get; set; }

        public sealed class STOutStats
        {
            //[JsonProperty("LG")]
            //public IList<GameDto> CompletedGames { get; set; }

            //[JsonProperty("N")]
            //public IList<SeasonDto> Seasons { get; set; }

            [JsonProperty("S")]
            public IList<StageDtoMod> Stages { get; set; }
        }
    }
}