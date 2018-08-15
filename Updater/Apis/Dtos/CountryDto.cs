using System.Collections.Generic;
using Insight.Database;
using Newtonsoft.Json;
using Updater.Apis.Dtos.Base;
using Updater.Common;

namespace Updater.Apis.Dtos
{
    public class CountryDto : BaseDtoWithTitle
    {
        [JsonProperty("I")]
        public string XCountryId { get; set; }

        [JsonProperty("N")]
        public override string Title { get; set; }

        [JsonProperty("C")]
        public int XBetId { get; set; }

        [ChildRecords]
        [JsonProperty("T")]
        public IList<LeagueDto> Leagues { get; set; }

        [JsonConstructor]
        public CountryDto(string i, string n, int c)
        {
            XCountryId = i;
            Title      = n;
            XBetId     = c;
            Id = CalculateHash(i);
        }

        public CountryDto()
        {
        }

        public override string ToString()
        {
            return $"[{Id}] {Title}";
        }
    }
}