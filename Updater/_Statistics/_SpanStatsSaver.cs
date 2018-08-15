//using System.Collections.Generic;
//using Insight.Database;
//using Updater.Apis.Dtos;
//using Updater.Entities;
//using Updater.Repositories;

//namespace Updater.Statistics
//{
//	public class SpanStatsSaver
//	{
//		private readonly DbInsight _db = new DbInsight();

//		public IList<SpanStats> GetSpanList(Game game)
//		{
//			var dateFrom = game.DateStart.Date;


//			var homeTeamGameList = _db.Connection.QuerySql<GameDto>(
//				"SELECT * FROM [Games] WHERE [DateStart] < @DateStart AND ([TeamAwayId] = @TeamHomeId OR [TeamHomeId] = @TeamHomeId)",
//				new {game.DateStart, game.TeamHome.Id});

//			var awayTeamGameList = _db.Connection.QuerySql<GameDto>(
//				"SELECT * FROM [Games] WHERE [DateStart] < @DateStart AND ([TeamAwayId] = @TeamAwayId OR [TeamHomeId] = @TeamAwayId)",
//				new {game.DateStart, game.TeamAway.Id});

//			var leagueGameList = _db.Connection.QuerySql<GameDto>(
//				"SELECT * FROM [Games] WHERE [DateStart] < @DateStart AND [LeagueId] = @LeagueId",
//				new {game.DateStart, game.League.Id});

//			return null;
//		}

//		public IList<SpanStats> GetSpanList(int gameId)
//		{
//			var mapping =
//				new OneToOne<Game, Country, League, Season, Stage, Team, Team, Period, Period, Period, Period,
//					Period, Period>(
//					(g, c, l, se, st, ht, at, p1, p2, p3, p4, ot, ttl) =>
//					{
//						g.Country    = c;
//						g.League     = l;
//						l.CountryId  = c.Id;
//						g.Season     = se;
//						se.CountryId = c.Id;
//						se.LeagueId  = l.Id;
//						g.Stage      = st;
//						st.CountryId = c.Id;
//						st.LeagueId  = l.Id;
//						st.SeasonId  = se.Id;
//						g.TeamHome   = ht;
//						g.TeamAway   = at;
//						g.P1         = p1;
//						g.P2         = p2;
//						g.P3         = p3;
//						g.P4         = p4;
//						g.OT         = ot;
//						g.Total      = ttl;
//					});

//			var reader = _db.Connection.QuerySql(
//				"SELECT Game.Id, DateStart, Country.Id, Country.Title, League.Id, League.Title, Season.Id, Season.Title, Stage.Id, Stage.Title, " +
//				"TeamHome.Id, TeamHome.Title, TeamHome.CountryId, TeamAway.Id, TeamAway.Title, TeamAway.CountryId, Game.P1_Home AS " +
//				"Home, Game.P1_Away AS Away, Game.P2_Home AS Home, Game.P2_Away AS Away, Game.P3_Home AS Home, Game.P3_Away AS " +
//				"Away, Game.P4_Home AS Home, Game.P4_Away AS Away, Game.OT_Home AS Home, Game.OT_Away AS Away, Game.Total_Home AS " +
//				"Home, Game.Total_Away AS Away FROM[Games] Game JOIN[Countries] Country ON(Country.Id = Game.CountryId) " +
//				"JOIN[Leagues] League ON(League.Id = Game.LeagueId) JOIN[Seasons] Season ON(Season.Id = Game.SeasonId) " +
//				"JOIN[Stages] Stage ON(Stage.Id = Game.StageId) JOIN[Teams] TeamHome ON(TeamHome.Id = Game.TeamHomeId) " +
//				"JOIN[Teams] TeamAway ON(TeamAway.Id = Game.TeamAwayId) WHERE Game.Id = @Id",
//				new {Id = gameId}, Query.ReturnsSingle(mapping));

//			return GetSpanList(reader);
//		}
//	}
//}