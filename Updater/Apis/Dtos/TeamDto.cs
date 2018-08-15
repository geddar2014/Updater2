using Newtonsoft.Json;
using Updater.Apis.Dtos.Base;
using Updater.Common;

namespace Updater.Apis.Dtos
{
    public class TeamDto : BaseDtoWithTitle
    {
        [JsonProperty("I")]
        public string XTeamId { get; set; }

        [JsonProperty("T")]
        public override string Title { get; set; }

        [JsonProperty("XI")]
        public int XBetId { get; set; }

        public TeamDto()
        {
        }

        [JsonConstructor]
        public TeamDto(string i, string t, int xi)
        {
            XTeamId = i;
            Title   = t;
            XBetId  = xi;
        }
    }
}