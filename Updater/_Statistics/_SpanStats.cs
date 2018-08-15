//using System.Collections.Generic;
//using System.Linq;
//using Updater.Apis.Dtos;

//namespace Updater.Statistics
//{
//	public class SpanStats
//	{
//		public static int MIN_STATS_THRESHOLD = 4;

//		public static int GAMES_PRECEEDS_SEASONS = 20;

//		public SpanStats()
//		{
//		}

//		public SpanStats(SpanType type,
//		                 string description,
//		                 IList<GameDto> gamesInLeague,
//		                 IList<GameDto> htGames,
//		                 IList<GameDto> atGames,
//		                 GameDto lastGame = null)
//		{
//			SpanType = type;

//			var leagueGames = gamesInLeague
//					.Where(l => l.State == 1 &&
//					            l.P1_Home > 0 && l.P1_Away > 0 &&
//					            l.P2_Home > 0 && l.P2_Away > 0 &&
//					            l.P3_Home > 0 && l.P3_Away > 0 &&
//					            l.P4_Home > 0 && l.P4_Away > 0)
//					.OrderBy(x => x.DateStart)
//					.ThenBy(x => x.Id)
//					.ToList();

//			lastGame = lastGame ?? leagueGames.Last();

//			GameId = lastGame.Id;

//			FirstGameId = leagueGames.First().Id;

//			var homeTeamId = lastGame.TeamHomeId;

//			var awayTeamId = lastGame.TeamAwayId;

//			var homeTeamGames = htGames
//					.Where(l => l.State == 1 &&
//					            l.P1_Home > 0 && l.P1_Away > 0 &&
//					            l.P2_Home > 0 && l.P2_Away > 0 &&
//					            l.P3_Home > 0 && l.P3_Away > 0 &&
//					            l.P4_Home > 0 && l.P4_Away > 0 &&
//					            (l.TeamHomeId == homeTeamId || l.TeamAwayId == homeTeamId))
//					.OrderBy(x => x.DateStart)
//					.ThenBy(x => x.Id)
//					.ToList();

//			var awayTeamGames = atGames
//					.Where(l => l.State == 1 &&
//					            l.P1_Home > 0 && l.P1_Away > 0 &&
//					            l.P2_Home > 0 && l.P2_Away > 0 &&
//					            l.P3_Home > 0 && l.P3_Away > 0 &&
//					            l.P4_Home > 0 && l.P4_Away > 0 &&
//					            (l.TeamHomeId == awayTeamId || l.TeamAwayId == awayTeamId))
//					.OrderBy(x => x.DateStart)
//					.ThenBy(x => x.Id)
//					.ToList();

//			#region LeagueGamesStats

//			League_GP_Count = leagueGames.Count;

//			var gameplays = League_GP_Count;

//			if (gameplays >= MIN_STATS_THRESHOLD)
//			{
//				P1_League_BT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P1_Home + x.P1_Away)) /
//				                       gameplays * 100;
//				P1_League_HT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P1_Home)) /
//				                       gameplays * 100;
//				P1_League_AT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P1_Away)) /
//				                       gameplays * 100;

//				P2_League_BT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P2_Home + x.P2_Away)) /
//				                       gameplays * 100;
//				P2_League_HT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P2_Home)) /
//				                       gameplays * 100;
//				P2_League_AT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P2_Away)) /
//				                       gameplays * 100;

//				P3_League_BT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P3_Home + x.P3_Away)) /
//				                       gameplays * 100;
//				P3_League_HT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P3_Home)) /
//				                       gameplays * 100;
//				P3_League_AT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P3_Away)) /
//				                       gameplays * 100;

//				P4_League_BT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P4_Home + x.P4_Away)) /
//				                       gameplays * 100;
//				P4_League_HT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P4_Home)) /
//				                       gameplays * 100;
//				P4_League_AT_Percent = (double) leagueGames.Count(x => x.HasSameOdd(x.P4_Away)) /
//				                       gameplays * 100;
//			}

//			#endregion

//			#region HomeTeam Any Games Stats

//			HT_AtAny_Count = homeTeamGames.Count;

//			if (HT_AtAny_Count >= MIN_STATS_THRESHOLD)
//			{
//				P1_HT_AtAny_Percent = (double) homeTeamGames
//						                      .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P1_Home)
//						                                  || x.TeamAwayId == homeTeamId && x.HasSameOdd(x.P1_Away)) /
//				                      homeTeamGames.Count * 100;

//				P2_HT_AtAny_Percent = (double) homeTeamGames
//						                      .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P2_Home)
//						                                  || x.TeamAwayId == homeTeamId && x.HasSameOdd(x.P2_Away)) /
//				                      homeTeamGames.Count * 100;

//				P3_HT_AtAny_Percent = (double) homeTeamGames
//						                      .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P3_Home)
//						                                  || x.TeamAwayId == homeTeamId && x.HasSameOdd(x.P3_Away)) /
//				                      homeTeamGames.Count * 100;

//				P4_HT_AtAny_Percent = (double) homeTeamGames
//						                      .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P4_Home)
//						                                  || x.TeamAwayId == homeTeamId && x.HasSameOdd(x.P4_Away)) /
//				                      homeTeamGames.Count * 100;
//			}

//			#endregion

//			#region HomeTeam Home Games Stats

//			var htAtHomeGames = homeTeamGames
//					.Where(l => l.TeamHomeId == homeTeamId)
//					.ToList();

//			HT_AtHome_Count = htAtHomeGames.Count;

//			if (HT_AtHome_Count >= MIN_STATS_THRESHOLD)
//			{
//				P1_HT_AtHome_Percent = (double) htAtHomeGames
//						                       .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P1_Home)) /
//				                       htAtHomeGames.Count * 100;

//				P2_HT_AtHome_Percent = (double) htAtHomeGames
//						                       .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P2_Home)) /
//				                       htAtHomeGames.Count * 100;

//				P3_HT_AtHome_Percent = (double) htAtHomeGames
//						                       .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P3_Home)) /
//				                       htAtHomeGames.Count * 100;

//				P4_HT_AtHome_Percent = (double) htAtHomeGames
//						                       .Count(x => x.TeamHomeId == homeTeamId && x.HasSameOdd(x.P4_Home)) /
//				                       htAtHomeGames.Count * 100;
//			}

//			#endregion

//			#region AwayTeam Any Games Stats

//			AT_AtAny_Count = awayTeamGames.Count;

//			if (AT_AtAny_Count >= MIN_STATS_THRESHOLD)
//			{
//				P1_AT_AtAny_Percent = (double) awayTeamGames
//						                      .Count(x => x.TeamHomeId == awayTeamId && x.HasSameOdd(x.P1_Home)
//						                                  || x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P1_Away)) /
//				                      awayTeamGames.Count * 100;

//				P2_AT_AtAny_Percent = (double) awayTeamGames
//						                      .Count(x => x.TeamHomeId == awayTeamId && x.HasSameOdd(x.P2_Home)
//						                                  || x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P2_Away)) /
//				                      awayTeamGames.Count * 100;

//				P3_AT_AtAny_Percent = (double) awayTeamGames
//						                      .Count(x => x.TeamHomeId == awayTeamId && x.HasSameOdd(x.P3_Home)
//						                                  || x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P3_Away)) /
//				                      awayTeamGames.Count * 100;

//				P4_AT_AtAny_Percent = (double) awayTeamGames
//						                      .Count(x => x.TeamHomeId == awayTeamId && x.HasSameOdd(x.P4_Home)
//						                                  || x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P4_Away)) /
//				                      awayTeamGames.Count * 100;
//			}

//			#endregion

//			#region AwayTeam Away Games Stats

//			var atAtAwayGames = awayTeamGames
//					.Where(l => l.TeamAwayId == awayTeamId)
//					.ToList();

//			AT_AtAway_Count = atAtAwayGames.Count;

//			if (AT_AtAway_Count >= MIN_STATS_THRESHOLD)
//			{
//				P1_AT_AtAway_Percent = (double) atAtAwayGames
//						                       .Count(x => x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P1_Away)) /
//				                       atAtAwayGames.Count * 100;

//				P2_AT_AtAway_Percent = (double) atAtAwayGames
//						                       .Count(x => x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P2_Away)) /
//				                       atAtAwayGames.Count * 100;

//				P3_AT_AtAway_Percent = (double) atAtAwayGames
//						                       .Count(x => x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P3_Away)) /
//				                       atAtAwayGames.Count * 100;

//				P4_AT_AtAway_Percent = (double) atAtAwayGames
//						                       .Count(x => x.TeamAwayId == awayTeamId && x.HasSameOdd(x.P4_Away)) /
//				                       atAtAwayGames.Count * 100;
//			}

//			#endregion

//			#region HTvsAT Any Games Stats

//			var htVsAtAnyGames = homeTeamGames
//					.Where(h => h.TeamHomeId == awayTeamId || h.TeamAwayId == awayTeamId)
//					.ToList();

//			HTvsAT_AtAny_Count = htVsAtAnyGames.Count;

//			if (HTvsAT_AtAny_Count >= MIN_STATS_THRESHOLD)
//			{
//				P1_HTvsAT_AtAny_Percent =
//						(double) htVsAtAnyGames.Count(x => x.HasSameOdd(x.P1_Home + x.P1_Away)) /
//						HTvsAT_AtAny_Count * 100;

//				P2_HTvsAT_AtAny_Percent =
//						(double) htVsAtAnyGames.Count(x => x.HasSameOdd(x.P2_Home + x.P2_Away)) /
//						HTvsAT_AtAny_Count * 100;

//				P3_HTvsAT_AtAny_Percent =
//						(double) htVsAtAnyGames.Count(x => x.HasSameOdd(x.P3_Home + x.P3_Away)) /
//						HTvsAT_AtAny_Count * 100;

//				P4_HTvsAT_AtAny_Percent =
//						(double) htVsAtAnyGames.Count(x => x.HasSameOdd(x.P4_Home + x.P4_Away)) /
//						HTvsAT_AtAny_Count * 100;
//			}

//			#endregion

//			#region HTvsAT Alike Games Stats

//			var htVsAtAlikeGames = htAtHomeGames
//					.Where(h => h.TeamAwayId == awayTeamId)
//					.ToList();

//			HTvsAT_AtAlike_Count = htVsAtAlikeGames.Count;

//			if (HTvsAT_AtAlike_Count >= MIN_STATS_THRESHOLD)
//			{
//				P1_HTvsAT_AtAlike_Percent =
//						(double) htVsAtAlikeGames.Count(x => x.HasSameOdd(x.P1_Home + x.P1_Away)) /
//						HTvsAT_AtAlike_Count * 100;

//				P2_HTvsAT_AtAlike_Percent =
//						(double) htVsAtAlikeGames.Count(x => x.HasSameOdd(x.P2_Home + x.P2_Away)) /
//						HTvsAT_AtAlike_Count * 100;

//				P3_HTvsAT_AtAlike_Percent =
//						(double) htVsAtAlikeGames.Count(x => x.HasSameOdd(x.P3_Home + x.P3_Away)) /
//						HTvsAT_AtAlike_Count * 100;

//				P4_HTvsAT_AtAlike_Percent =
//						(double) htVsAtAlikeGames.Count(x => x.HasSameOdd(x.P4_Home + x.P4_Away)) /
//						HTvsAT_AtAlike_Count * 100;
//			}

//			#endregion

//			if (gameplays >= MIN_STATS_THRESHOLD)
//			{
//				#region P2P1 повторы

//				var p2p1List = gamesInLeague.Where(x =>
//								x.HasSameOdd(x.P2_Home + x.P2_Away) &&
//								x.HasSameOdd(x.P1_Home + x.P1_Away))
//						.ToList();

//				P2P1_League_Count = p2p1List.Count;

//				P2P1_League_Percent = (double) P2P1_League_Count / League_GP_Count * 100;

//				if (P2P1_League_Count > 0)
//					P2P1_League_GamesAway = leagueGames.Count - 1 - leagueGames
//							                        .FindLastIndex(
//									                        x => x.HasSameOdd(x.P2_Home + x.P2_Away) &&
//									                             x.HasSameOdd(x.P1_Home + x.P1_Away));

//				#endregion

//				#region P3P2P1 повторы

//				var p3p2p1List = gamesInLeague.Where(x =>
//								x.HasSameOdd(x.P3_Home + x.P3_Away) &&
//								x.HasSameOdd(x.P2_Home + x.P2_Away) &&
//								x.HasSameOdd(x.P1_Home + x.P1_Away))
//						.ToList();

//				P3P2P1_League_Count = p3p2p1List.Count;

//				P3P2P1_League_Percent = (double) P3P2P1_League_Count / League_GP_Count * 100;

//				if (P3P2P1_League_Count > 0)
//					P3P2P1_League_GamesAway =
//							leagueGames.Count - 1 - leagueGames.FindLastIndex(x =>
//									x.HasSameOdd(x.P3_Home + x.P3_Away) &&
//									x.HasSameOdd(x.P2_Home + x.P2_Away) &&
//									x.HasSameOdd(x.P1_Home + x.P1_Away));

//				#endregion

//				#region P4P3P2P1 повторы

//				var p4p3p2p1List = gamesInLeague.Where(x =>
//								x.HasSameOdd(x.P4_Home + x.P4_Away) &&
//								x.HasSameOdd(x.P3_Home + x.P3_Away) &&
//								x.HasSameOdd(x.P2_Home + x.P2_Away) &&
//								x.HasSameOdd(x.P1_Home + x.P1_Away))
//						.ToList();

//				P4P3P2P1_League_Count = p4p3p2p1List.Count;

//				P4P3P2P1_League_Percent = (double) P4P3P2P1_League_Count / League_GP_Count * 100;

//				if (P4P3P2P1_League_Count > 0)
//					P4P3P2P1_League_GamesAway =
//							leagueGames.Count - 1 - leagueGames.FindLastIndex(x =>
//									x.HasSameOdd(x.P4_Home + x.P4_Away) &&
//									x.HasSameOdd(x.P3_Home + x.P3_Away) &&
//									x.HasSameOdd(x.P2_Home + x.P2_Away) &&
//									x.HasSameOdd(x.P1_Home + x.P1_Away));

//				#endregion

//				#region PnotPprev Percent

//				P2notP1_BT_Percent = (double) gamesInLeague.Count(x =>
//						                     (x.P2_Home + x.P2_Away) % 2 != (x.P1_Home + x.P1_Away) % 2) /
//				                     leagueGames.Count * 100;

//				P2notP1_HT_Percent = (double) gamesInLeague.Count(x =>
//						                     x.P2_Home % 2 != x.P1_Home % 2) /
//				                     leagueGames.Count * 100;

//				P2notP1_AT_Percent = (double) gamesInLeague.Count(x =>
//						                     x.P2_Away % 2 != x.P1_Away % 2) /
//				                     leagueGames.Count * 100;

//				P3notP2_BT_Percent = (double) gamesInLeague.Count(x =>
//						                     (x.P3_Home + x.P3_Away) % 2 != (x.P2_Home + x.P2_Away) % 2) /
//				                     leagueGames.Count * 100;

//				P3notP2_HT_Percent = (double) gamesInLeague.Count(x =>
//						                     x.P3_Home % 2 != x.P2_Home % 2) /
//				                     leagueGames.Count * 100;

//				P3notP2_AT_Percent = (double) gamesInLeague.Count(x =>
//						                     x.P3_Away % 2 != x.P2_Away % 2) /
//				                     leagueGames.Count * 100;

//				P4notP3_BT_Percent = (double) gamesInLeague.Count(x =>
//						                     (x.P4_Home + x.P4_Away) % 2 != (x.P3_Home + x.P3_Away) % 2) /
//				                     leagueGames.Count * 100;

//				P4notP3_HT_Percent = (double) gamesInLeague.Count(x =>
//						                     x.P4_Home % 2 != x.P3_Home % 2) /
//				                     leagueGames.Count * 100;

//				P4notP3_AT_Percent = (double) gamesInLeague.Count(x =>
//						                     x.P4_Away % 2 != x.P3_Away % 2) /
//				                     leagueGames.Count * 100;

//				#endregion
//			}
//		}

//		public string      GameId          { get; }
//		public string      FirstGameId     { get; }
//		public string   Description     { get; }
//		public SpanType SpanType        { get; }
//		public int      League_GP_Count { get; }

//		public double? P1_League_BT_Percent { get; }
//		public double? P1_League_HT_Percent { get; }
//		public double? P1_League_AT_Percent { get; }

//		public double? P2_League_BT_Percent { get; }
//		public double? P2_League_HT_Percent { get; }
//		public double? P2_League_AT_Percent { get; }

//		public double? P3_League_BT_Percent { get; }
//		public double? P3_League_HT_Percent { get; }
//		public double? P3_League_AT_Percent { get; }

//		public double? P4_League_BT_Percent { get; }
//		public double? P4_League_HT_Percent { get; }
//		public double? P4_League_AT_Percent { get; }

//		public double? P2notP1_BT_Percent { get; }
//		public double? P2notP1_HT_Percent { get; }
//		public double? P2notP1_AT_Percent { get; }

//		public double? P3notP2_BT_Percent { get; }
//		public double? P3notP2_HT_Percent { get; }
//		public double? P3notP2_AT_Percent { get; }

//		public double? P4notP3_BT_Percent { get; }
//		public double? P4notP3_HT_Percent { get; }
//		public double? P4notP3_AT_Percent { get; }

//		public int     P2P1_League_Count     { get; }
//		public double? P2P1_League_Percent   { get; }
//		public int?    P2P1_League_GamesAway { get; }

//		public int     P3P2P1_League_Count     { get; }
//		public double? P3P2P1_League_Percent   { get; }
//		public int?    P3P2P1_League_GamesAway { get; }

//		public int     P4P3P2P1_League_Count     { get; }
//		public double? P4P3P2P1_League_Percent   { get; }
//		public int?    P4P3P2P1_League_GamesAway { get; }

//		public int     HT_AtAny_Count      { get; }
//		public double? P1_HT_AtAny_Percent { get; }
//		public double? P2_HT_AtAny_Percent { get; }
//		public double? P3_HT_AtAny_Percent { get; }
//		public double? P4_HT_AtAny_Percent { get; }

//		public int     HT_AtHome_Count      { get; }
//		public double? P1_HT_AtHome_Percent { get; }
//		public double? P2_HT_AtHome_Percent { get; }
//		public double? P3_HT_AtHome_Percent { get; }
//		public double? P4_HT_AtHome_Percent { get; }

//		public int     AT_AtAny_Count      { get; }
//		public double? P1_AT_AtAny_Percent { get; }
//		public double? P2_AT_AtAny_Percent { get; }
//		public double? P3_AT_AtAny_Percent { get; }
//		public double? P4_AT_AtAny_Percent { get; }

//		public int     AT_AtAway_Count      { get; }
//		public double? P1_AT_AtAway_Percent { get; }
//		public double? P2_AT_AtAway_Percent { get; }
//		public double? P3_AT_AtAway_Percent { get; }
//		public double? P4_AT_AtAway_Percent { get; }

//		public int     HTvsAT_AtAny_Count      { get; }
//		public double? P1_HTvsAT_AtAny_Percent { get; }
//		public double? P2_HTvsAT_AtAny_Percent { get; }
//		public double? P3_HTvsAT_AtAny_Percent { get; }
//		public double? P4_HTvsAT_AtAny_Percent { get; }

//		public int     HTvsAT_AtAlike_Count      { get; }
//		public double? P1_HTvsAT_AtAlike_Percent { get; }
//		public double? P2_HTvsAT_AtAlike_Percent { get; }
//		public double? P3_HTvsAT_AtAlike_Percent { get; }
//		public double? P4_HTvsAT_AtAlike_Percent { get; }
//	}
//}