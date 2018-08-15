//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using Newtonsoft.Json;
//using Shouldly;
//using Updater.Apis.Dtos;
//using Xunit;

//namespace Test
//{
//	public class UpdaterTests
//	{
//		[Fact]
//		public void Should_Deserialize_Great()
//		{
//			var list = JsonConvert.DeserializeObject<IList<GetDayStatsOutput>>(File.ReadAllText(
//					Path.Combine(Directory.GetCurrentDirectory(), "Json", "GameByDay.json")));

//			list.Count.ShouldBe(2);

//			var first = list.First();


//			first.StageDto.Id.ShouldBe(239928);
//			first.StageDto.Title.ShouldBe("США: WNBA. Регулярный чемпионат");
//			first.StageDto.SeasonId.ShouldBe(115680);
//			first.StageDto.CountryId.ShouldBe(200);
//			first.StageDto.LeagueId.ShouldBe(963);

//			first.Games.Count.ShouldBe(3);
//			var game = first.Games[1];
//			game.Id.ShouldBe(26600783);
//			game.CountryId.ShouldBe(200);
//			game.SeasonId.ShouldBe(115680);
//			game.LeagueId.ShouldBe(963);
//			game.StageId.ShouldBe(239928);

//			game.DateStart.Date.ShouldBe(DateTime.Parse("11.06.2018"));

//			game.Total_Home.ShouldBe(72);
//			game.TeamHomeId.ShouldBe(84644);
//			game.TeamAwayId.ShouldBe(84638);

//			game.State.ShouldBe(1);
//		}
//	}
//}