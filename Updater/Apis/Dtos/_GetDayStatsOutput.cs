using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Updater.Apis.Dtos
{
	//public class GetDayStatsOutput
	//{
	//	[JsonConstructor]
	//	public GetDayStatsOutput(int champStageId, string stageTitle, int champSeasonId, int categoryId,
	//	                         int champId,
	//	                         IList<GameDto> games)
	//	{
	//		StageDto = new StageDto(champStageId, stageTitle, champSeasonId, categoryId, champId);
	//
	//		Games = games;
	//
	//		foreach (var g in games)
	//		{
	//			g.CountryId = categoryId;
	//			g.LeagueId  = champId;
	//			g.SeasonId  = champSeasonId;
	//			g.StageId   = champStageId;
	//		}
	//
	//		Teams = games.SelectMany(g => new[] {g.TeamHome, g.TeamAway})
	//				.GroupBy(t => t.Id)
	//				.Select(x => x.First())
	//				.ToList();
	//	}
	//
	//	[JsonIgnore]
	//	public StageDto StageDto { get; set; }
	//
	//
	//	[JsonProperty("Games")]
	//	public IList<GameDto> Games { get; set; }
	//
	//	[JsonIgnore]
	//	public IList<TeamDto> Teams { get; set; }
	//}
}