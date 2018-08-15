using System;
using System.Collections.Generic;
using System.Linq;
using Insight.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Updater.Apis.Dtos.Base;
using Updater.Common;
// ReSharper disable InconsistentNaming

namespace Updater.Apis.Dtos
{
    [Recordset(typeof(GameDto), typeof(TeamDto), typeof(TeamDto))]
    [BindChildren(BindChildrenFor.All)]
    public class GameDto : BaseDto
    {
        [ParentRecordId]
        [JsonIgnore]
        public string ParentId { get; set; }

        [JsonProperty("I")]
        public string XGameId { get; set; }

        [JsonProperty("SI")]
        public string XStageId { get; set; }

        [JsonConstructor]
        public GameDto(TeamDto          a,
                       int              d,
                       TeamDto          h,
                       string           i,
                       IList<PeriodDto> p,
                       int              s1,
                       int              s2,
                       string           si,
                       int              st)
        {
            TeamAwayId  = a.Id;
            XTeamAwayId = a.XTeamId;
            TeamHomeId  = h.Id;
            XTeamHomeId = h.XTeamId;
            Id = XGameId.ToMD5Hash();
            ParentId = XStageId.ToMD5Hash();

            for (var n = 1; n <= p.Count; n++)
            {
                switch (n)
                {
                    case 1:
                        P1_Home = p[n].Home;
                        P1_Away = p[n].Away;
                        break;
                    case 2:
                        P2_Home = p[n].Home;
                        P2_Away = p[n].Away;
                        break;
                    case 3:
                        P3_Home = p[n].Home;
                        P3_Away = p[n].Away;
                        break;
                    case 4:
                        P4_Home = p[n].Home;
                        P4_Away = p[n].Away;
                        break;
                    case 5:
                        OT_Home = p[n].Home;
                        OT_Away = p[n].Away;
                        break;
                    default: break;
                }
            }
        }

        public GameDto()
        {
        }


        [JsonProperty("H")]
        public TeamDto TeamHome { get; set; }

        [JsonProperty("A")]
        public TeamDto TeamAway { get; set; }

        [JsonIgnore]
        public int P1_Home { get; set; }

        [JsonIgnore]
        public int P1_Away { get; set; }

        [JsonIgnore]
        public int P2_Home { get; set; }

        [JsonIgnore]
        public int P2_Away { get; set; }

        [JsonIgnore]
        public int P3_Home { get; set; }

        [JsonIgnore]
        public int P3_Away { get; set; }

        [JsonIgnore]
        public int P4_Home { get; set; }

        [JsonIgnore]
        public int P4_Away { get; set; }

        [JsonIgnore]
        public int OT_Home { get; set; }

        [JsonIgnore]
        public int OT_Away { get; set; }

        [JsonProperty("S1")]
        public int Total_Home { get; set; }

        [JsonProperty("S2")]
        public int Total_Away { get; set; }

        [JsonIgnore]
        public string XTeamHomeId { get; set; }

        [JsonIgnore]
        public string TeamHomeId { get; set; }

        [JsonIgnore]
        public string XTeamAwayId { get; set; }

        [JsonIgnore]
        public string TeamAwayId { get; set; }

        [JsonProperty("D")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime DateStart { get; set; }

        [JsonProperty("St")]
        public int State { get; set; }
    }
}