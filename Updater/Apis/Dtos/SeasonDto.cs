using Insight.Database;
using Newtonsoft.Json;
using Updater.Apis.Dtos.Base;

namespace Updater.Apis.Dtos
{
    public class SeasonDto : BaseDtoWithTitle
    {
        [JsonIgnore]
        public string XCountryId { get; set; }

        [JsonIgnore]
        public string XLeagueId { get;set; }

        [JsonProperty("I")]
        public string XSeasonId { get; set; }
        

        [JsonProperty("T")]
        public override string Title { get; set; }

        [ParentRecordId]
        [JsonIgnore]
        public string ParentId { get; set; }

        [JsonConstructor]
        public SeasonDto(string i, string t)
        {
            XSeasonId   = i;
            Title = t;
        }

        public SeasonDto()
        {
        }
    }
}