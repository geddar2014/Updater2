//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Updater.Apis.Dtos;
//using Updater.Statistics;

//namespace Test
//{
//	public class StatisticsTests
//	{
//		private readonly IList<GameDto> _testData1_A;
//		private readonly IList<GameDto> _testData1_H;
//		private readonly IList<GameDto> _testData1_League;
//		private readonly IList<GameDto> _testData2_League;

//		public StatisticsTests()
//		{
//			_testData1_League = new List<GameDto>
//			{
//					// ODD

//					new GameDto
//					{
//							Id        = 1,
//							CountryId = 200,
//							LeagueId  = 300,
//							SeasonId  = 33,
//							StageId   = 44,

//							TeamHomeId = 11,
//							TeamAwayId = 22,
//							State      = 1,

//							DateStart = DateTime.Now.Date.AddYears(-11),

//							// ODD
//							P1_Home = 15,
//							// EVEN
//							P1_Away = 26,

//							// EVEN
//							P2_Home = 14,
//							// ODD
//							P2_Away = 19,

//							// ODD
//							P3_Home = 25,
//							// EVEN
//							P3_Away = 18,

//							// EVEN
//							P4_Home = 32,
//							// EVEN
//							P4_Away = 12,

//							/// GAME IS ODD

//							Total_Home = 86,
//							Total_Away = 75
//					},
//					new GameDto
//					{
//							Id        = 2,
//							CountryId = 200,
//							LeagueId  = 300,
//							SeasonId  = 33,
//							StageId   = 44,

//							TeamHomeId = 11,
//							TeamAwayId = 33,
//							State      = 1,

//							DateStart = DateTime.Now.Date.AddYears(-8),

//							P1_Home = 27,
//							P1_Away = 12,

//							P2_Home = 18,
//							P2_Away = 23,

//							P3_Home = 32,
//							P3_Away = 12,

//							P4_Home = 10,
//							P4_Away = 29,

//							Total_Home = 87,
//							Total_Away = 76
//					},
//					new GameDto
//					{
//							Id         = 3,
//							CountryId  = 200,
//							LeagueId   = 300,
//							SeasonId   = 44,
//							StageId    = 55,
//							TeamHomeId = 33,
//							TeamAwayId = 22,
//							State      = 1,

//							DateStart = DateTime.Now.Date.AddYears(-6),

//							P1_Home = 21,
//							P1_Away = 29,

//							P2_Home = 11,
//							P2_Away = 18,

//							P3_Home = 17,
//							P3_Away = 9,

//							P4_Home = 5,
//							P4_Away = 31,

//							Total_Home = 54,
//							Total_Away = 87
//					},
//					new GameDto
//					{
//							Id         = 4,
//							CountryId  = 200,
//							LeagueId   = 300,
//							SeasonId   = 44,
//							StageId    = 55,
//							TeamHomeId = 11,
//							TeamAwayId = 22,
//							State      = 1,

//							DateStart = DateTime.Now.Date.AddYears(-3),

//							P1_Home = 12,
//							P1_Away = 17,

//							P2_Home = 23,
//							P2_Away = 28,

//							P3_Home = 18,
//							P3_Away = 6,

//							P4_Home = 15,
//							P4_Away = 4,

//							Total_Home = 68,
//							Total_Away = 55
//					},
//					new GameDto
//					{
//							Id         = 5,
//							CountryId  = 200,
//							LeagueId   = 300,
//							SeasonId   = 44,
//							StageId    = 55,
//							TeamHomeId = 22,
//							TeamAwayId = 33,
//							State      = 1,

//							DateStart = DateTime.Now.Date.AddMonths(-11),

//							P1_Home = 23,
//							P1_Away = 21,

//							P2_Home = 17,
//							P2_Away = 15,

//							P3_Home = 5,
//							P3_Away = 31,

//							P4_Home = 8,
//							P4_Away = 48,

//							Total_Home = 53,
//							Total_Away = 115
//					},
//					new GameDto
//					{
//							Id         = 6,
//							CountryId  = 200,
//							LeagueId   = 300,
//							SeasonId   = 44,
//							StageId    = 55,
//							TeamHomeId = 22,
//							TeamAwayId = 33,
//							State      = 2,

//							DateStart = DateTime.Now.Date
//					}
//			};
//			_testData1_H = _testData1_League
//					.Where(x => x.State == 1 && (x.TeamHomeId == 22 || x.TeamAwayId == 22)).ToList();
//			_testData1_A = _testData1_League
//					.Where(x => x.State == 1 && (x.TeamHomeId == 33 || x.TeamAwayId == 33)).ToList();

//			_testData2_League = _testData1_League.Take(4).ToList();

//			SpanStats.MIN_STATS_THRESHOLD = 2;
//		}

//		/*
//		[Fact]
//		public void Should_LeagueGames_Construct_Correctly()
//		{
//			_testData1_H.Count.ShouldBe(4);
//			_testData1_A.Count.ShouldBe(3);

//			var statSpan = new SpanStats(SpanType.AllTime, _testData1_League, _testData1_H, _testData1_A);

//			statSpan.League_GP_Count.ShouldBe(5);

//			statSpan.P1_League_BT_Percent.ShouldNotBeNull();
//			statSpan.P1_League_BT_Percent.ShouldBe(4d / 5 * 100);
//			statSpan.P1_League_HT_Percent.ShouldBe(3d / 5 * 100);
//			statSpan.P1_League_AT_Percent.ShouldBe(2d / 5 * 100);

//			statSpan.P2_League_BT_Percent.ShouldNotBeNull();
//			statSpan.P2_League_BT_Percent.ShouldBe(5d / 5 * 100);
//			statSpan.P2_League_HT_Percent.ShouldBe(2d / 5 * 100);
//			statSpan.P2_League_AT_Percent.ShouldBe(2d / 5 * 100);

//			statSpan.P3_League_BT_Percent.ShouldNotBeNull();
//			statSpan.P3_League_BT_Percent.ShouldBe(2d / 5 * 100);
//			statSpan.P3_League_HT_Percent.ShouldBe(2d / 5 * 100);
//			statSpan.P3_League_AT_Percent.ShouldBe(1d / 5 * 100);

//			statSpan.P4_League_BT_Percent.ShouldNotBeNull();
//			statSpan.P4_League_BT_Percent.ShouldBe(3d / 5 * 100);
//			statSpan.P4_League_HT_Percent.ShouldBe(3d / 5 * 100);
//			statSpan.P4_League_AT_Percent.ShouldBe(3d / 5 * 100);


//			statSpan.HT_AtAny_Count.ShouldBe(4);
//			statSpan.P1_HT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P1_HT_AtAny_Percent?.ShouldBe(2d / 4 * 100);
//			statSpan.P2_HT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P2_HT_AtAny_Percent?.ShouldBe(1d / 4 * 100);
//			statSpan.P3_HT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P3_HT_AtAny_Percent?.ShouldBe(1d / 4 * 100);
//			statSpan.P4_HT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P4_HT_AtAny_Percent?.ShouldBe(2d / 4 * 100);


//			statSpan.HT_AtHome_Count.ShouldBe(1);
//			statSpan.P1_HT_AtHome_Percent.ShouldBeNull();
//			statSpan.P2_HT_AtHome_Percent.ShouldBeNull();
//			statSpan.P3_HT_AtHome_Percent.ShouldBeNull();
//			statSpan.P4_HT_AtHome_Percent.ShouldBeNull();


//			statSpan.AT_AtAny_Count.ShouldBe(3);
//			statSpan.P1_AT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P1_AT_AtAny_Percent?.ShouldBe(1d / 3 * 100);
//			statSpan.P2_AT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P2_AT_AtAny_Percent?.ShouldBe(2d / 3 * 100);
//			statSpan.P3_AT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P3_AT_AtAny_Percent?.ShouldBe(1d / 3 * 100);
//			statSpan.P4_AT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P4_AT_AtAny_Percent?.ShouldBe(3d / 3 * 100);


//			statSpan.AT_AtAway_Count.ShouldBe(2);
//			statSpan.P1_AT_AtAway_Percent.ShouldNotBeNull();
//			statSpan.P1_AT_AtAway_Percent?.ShouldBe(0d / 2 * 100);
//			statSpan.P2_AT_AtAway_Percent.ShouldNotBeNull();
//			statSpan.P2_AT_AtAway_Percent?.ShouldBe(1d / 2 * 100);
//			statSpan.P3_AT_AtAway_Percent.ShouldNotBeNull();
//			statSpan.P3_AT_AtAway_Percent?.ShouldBe(0d / 2 * 100);
//			statSpan.P4_AT_AtAway_Percent.ShouldNotBeNull();
//			statSpan.P4_AT_AtAway_Percent?.ShouldBe(2d / 2 * 100);


//			statSpan.HTvsAT_AtAny_Count.ShouldBe(2);
//			statSpan.P1_HTvsAT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P1_HTvsAT_AtAny_Percent?.ShouldBe(1d / 2 * 100);
//			statSpan.P2_HTvsAT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P2_HTvsAT_AtAny_Percent?.ShouldBe(2d / 2 * 100);
//			statSpan.P3_HTvsAT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P3_HTvsAT_AtAny_Percent?.ShouldBe(1d / 2 * 100);
//			statSpan.P4_HTvsAT_AtAny_Percent.ShouldNotBeNull();
//			statSpan.P4_HTvsAT_AtAny_Percent?.ShouldBe(1d / 2 * 100);


//			statSpan.HTvsAT_AtAlike_Count.ShouldBe(1);
//			statSpan.P1_HTvsAT_AtAlike_Percent.ShouldBeNull();
//			statSpan.P2_HTvsAT_AtAlike_Percent.ShouldBeNull();
//			statSpan.P3_HTvsAT_AtAlike_Percent.ShouldBeNull();
//			statSpan.P4_HTvsAT_AtAlike_Percent.ShouldBeNull();
//		}

//		[Fact]
//		public void Should_P2P1_P3P2P1_Construct_Correctly()
//		{
//			var statSpan = new SpanStats(SpanType.AllTime, _testData2_League, _testData1_H, _testData1_A);

//			statSpan.League_GP_Count.ShouldBe(4);

//			statSpan.P2P1_League_Count.ShouldBe(3);
//			statSpan.P2P1_League_GamesAway.ShouldNotBeNull();
//			statSpan.P2P1_League_GamesAway?.ShouldBe(0);
//			statSpan.P2P1_League_Percent.ShouldNotBeNull();
//			statSpan.P2P1_League_Percent.ShouldBe(3d / 4 * 100);

//			statSpan.P3P2P1_League_Count.ShouldBe(1);
//			statSpan.P3P2P1_League_GamesAway.ShouldNotBeNull();
//			statSpan.P3P2P1_League_GamesAway?.ShouldBe(3);
//			statSpan.P3P2P1_League_Percent.ShouldNotBeNull();
//			statSpan.P3P2P1_League_Percent.ShouldBe(1d / 4 * 100);
//		}
//		*/
//	}
//}