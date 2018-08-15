using Newtonsoft.Json;

namespace Updater.Apis.Dtos
{
    public class PeriodDto
    {
        [JsonProperty("H")]
        public int Home { get; set; }

        [JsonProperty("A")]
        public int Away { get; set; }

        [JsonProperty("T")]
        public int Length { get; set; }
    }
}