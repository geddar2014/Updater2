//using Insight.Database;
//using Newtonsoft.Json;

//namespace Updater.Entities
//{
//	public class Season
//	{
//		[RecordId]
//		[JsonProperty("I")]
//		public string Id { get; set; }

//		[ParentRecordId]
//		[JsonIgnore]
//		public string LeagueId { get; set; }

//		[JsonProperty("T")]
//		public string Title { get; set; }

//		public override string ToString()
//		{
//			return $"[{Id}] {Title}";
//		}
//	}
//}