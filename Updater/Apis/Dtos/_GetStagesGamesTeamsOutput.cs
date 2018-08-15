using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Updater.Apis.Dtos
{
	//public class GetStagesGamesTeamsOutput
	//{
	//	[JsonConstructor]
	//	public GetStagesGamesTeamsOutput(IList<GameDto> games, IDictionary<int, StageDto> stages)
	//	{
	//		Games = games.GroupBy(t => t.Id).Select(t => t.First()).ToList();
	//		Teams = games.SelectMany(g => new[] {g.TeamHome, g.TeamAway}).GroupBy(t => t.Id).Select(t => t.First())
	//				.ToList();
	//		Stages = stages.Values.ToList();
	//	}
	//
	//	public IList<GameDto> Games { get; set; }
	//
	//	public IList<TeamDto> Teams { get; set; }
	//
	//	public IList<StageDto> Stages { get; set; }
	//}
}