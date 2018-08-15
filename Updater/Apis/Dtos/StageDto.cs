using System;
using System.Collections.Generic;
using Insight.Database;
using Newtonsoft.Json;
using Updater.Apis.Dtos.Base;

namespace Updater.Apis.Dtos
{
    public class StageDto : BaseDtoWithTitle
    {
        [JsonProperty("R")]
        public string XCountryId { get; set; }

        [JsonIgnore]
        public string XLeagueId { get; set; }

        [JsonIgnore]
        public string XSeasonId { get; set; }

        [JsonProperty("I")]
        public string XStageId { get; set; }


        [JsonProperty("N")]
        public override string Title { get; set; }

        [ParentRecordId]
        [JsonIgnore]
        public string ParentId { get; set; }

        [JsonConstructor]
        public StageDto(string i, string n, string r)
        {
            XStageId = i;
            Title = n;
            XCountryId = r;
        }

        public StageDto()
        {
        }

        public static explicit operator StageDto(List<StageDtoMod> v)
        {
            throw new NotImplementedException();
        }
    }
}