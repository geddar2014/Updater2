using Insight.Database;
using Newtonsoft.Json;
using Updater.Apis.Dtos.Base;
using Updater.Common;

namespace Updater.Apis.Dtos
{
    public class LeagueDto : BaseDtoWithTitle
    {
        [JsonIgnore]
        public string XCountryId { get; set; }

        [JsonProperty("I")]
        public string XLeagueId { get; set; }


        [JsonProperty("T")]
        public override string Title { get; set; }

        [ParentRecordId]
        [JsonIgnore]
        public string ParentId { get; set; }

        [JsonConstructor]
        public LeagueDto(string i, string t)
        {
            XLeagueId = i;
            Title     = t;
        }

        public LeagueDto()
        {
        }
    }
}