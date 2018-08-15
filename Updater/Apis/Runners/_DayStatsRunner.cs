namespace Updater.Apis.Runners
{
	//public class DayStatsRunner : Runner<DateArgs, IList<GetDayStatsOutput>>
	//{
	//	public DayStatsRunner(Result total = null) : base(total)
	//	{
	//		_total = total ?? new Result(DateArgs.Create(DateTime.Now), RunnerType.DayStats);
	//	}

	//	protected override async Task ProcessAsync(DateArgs args, Result result)
	//	{
	//		var url = string.Format("Sport/3/Game/{0:yyyy-M-d}", args.Date);

	//		var fetch = await GetAsync(url);

	//		if (fetch.Count > 0)
	//		{
	//			#region countries

	//			var countries = fetch.Select(x => x.StageDto.CountryId).Union(fetch.SelectMany(x => x.Games)
	//							.SelectMany(x =>
	//									new[] {x.CountryId, x.TeamHome.CountryId, x.TeamAway.CountryId})).Distinct()
	//					.Select(x => new CountryDto(x)).ToList();

	//			if (countries.Count > 0)
	//			{
	//				_db.CountryRepository.AddIfNotExists_Countries(countries, out var countriesInserted);
	//				result.CountriesInserted = countriesInserted;
	//			}

	//			#endregion

	//			#region leagues

	//			var leagues = fetch.Select(x => new LeagueDto(x.StageDto.LeagueId, x.StageDto.CountryId))
	//					.Union(fetch.SelectMany(x => x.Games)
	//							.Select(x => new LeagueDto(x.LeagueId, x.CountryId)))
	//					.GroupBy(x => x.Id)
	//					.Select(x => x.First())
	//					.ToList();

	//			if (leagues.Count > 0)
	//			{
	//				_db.LeagueRepository.AddIfNotExists_Leagues(leagues, out var leaguesInserted);
	//				result.LeaguesInserted = leaguesInserted;
	//			}

	//			#endregion

	//			#region seasons

	//			var seasons = fetch.Select(x =>
	//							new SeasonDto(x.StageDto.SeasonId, x.StageDto.CountryId, x.StageDto.LeagueId))
	//					.Union(fetch.SelectMany(x => x.Games)
	//							.Select(x => new SeasonDto(x.SeasonId, x.CountryId, x.LeagueId)))
	//					.GroupBy(x => x.Id).Select(x => x.First()).ToList();

	//			if (seasons.Count > 0)
	//			{
	//				_db.SeasonRepository.AddIfNotExists_Seasons(seasons, out var seasonsInserted);
	//				result.SeasonsInserted = seasonsInserted;
	//			}

	//			#endregion

	//			#region stages

	//			var stages = fetch.Select(x => x.StageDto).ToList();

	//			if (stages.Count > 0)
	//			{
	//				_db.StageRepository.AddOrUpdate_Stages(stages, out var stagesInserted, out var stagesUpdated);
	//				result.StagesInserted = stagesInserted;
	//				result.StagesUpdated  = stagesUpdated;
	//			}

	//			#endregion

	//			#region Teams

	//			var teams = fetch
	//					.SelectMany(x => x.Teams)
	//					.ToList();

	//			if (teams.Count > 0)
	//			{
	//				_db.TeamRepository.AddOrUpdate_Teams(teams, out var teamsInserted, out var teamsUpdated);
	//				result.TeamsInserted = teamsInserted;
	//				result.TeamsUpdated  = teamsUpdated;
	//			}

	//			#endregion

	//			#region Games

	//			var games = fetch
	//					.SelectMany(s => s.Games)
	//					.ToList();

	//			if (games.Count > 0)
	//			{
	//				_db.GameRepository.AddOrUpdate_Games(games, out var gamesInserted, out var gamesUpdated);
	//				result.GamesInserted = gamesInserted;
	//				result.GamesUpdated  = gamesUpdated;
	//			}

	//			#endregion
	//		}
	//	}

	//	public override async Task RunAsync()
	//	{
	//		var dbDates = _db.Connection
	//				.QuerySql<string>(
	//						$"SELECT Args FROM UpdateResults WHERE RunnerType = {(int) RunnerType.DayStats}")
	//				.Distinct().Select(JsonConvert.DeserializeObject<DateArgs>).Select(x => x.Date).OrderBy(x => x)
	//				.ToList();

	//		//var firstDbDate = _db.Connection.ExecuteScalarSql<DateTime>(
	//		//		"SELECT TOP 1 DateStart FROM Games ORDER BY DateStart ASC").Date;

	//		var lastDate = DateTime.Now.AddDays(2).Date;

	//		var dbNonUpdatedDates = _db.Connection.QuerySql<DateTime>(
	//				"SELECT DateStart FROM Games WHERE State<>1 ORDER BY DateStart ASC").Select(x => x.Date);


	//		//var liveDates = Enumerable.Range(0, 1 + lastDate.Subtract(firstDbDate).Days)
	//		//		.Select(offset => firstDbDate.AddDays(offset))
	//		//		.ToArray();

	//		var unfetchedDates =
	//				dbNonUpdatedDates.Select(DateArgs
	//						.Create); // liveDates.Except(dbDates).OrderBy(x => x).Select(DateArgs.Create).ToList();

	//		_totalSteps = unfetchedDates.Count();

	//		_currentStep = 0;
	//		var dt = DateTime.Now;
	//		await unfetchedDates.ForEachAsync(THREADS, async item => await Round(item));

	//		var log = $"{THREADS} threads: {(DateTime.Now - dt).Seconds}";

	//		Console.WriteLine(log);
	//		Log.Information(log);
	//	}

	//	public static void Run(Result total = null)
	//	{
	//		Task.Run(async () => await new DayStatsRunner(total).RunAsync()).Wait();
	//		//AsyncHelper.RunSync(async () => await new DayStatsRunner(total).RunAsync());
	//	}
	//}

	/*
	public class GameStatsRunner : Runner<GameIdArgs, IList<GetDayStatsOutput>>
	{
		public GameStatsRunner(Result total = null) : base(total)
		{
			_total = total ?? new Result(GameIdArgs.Create(0), RunnerType.GameStats);
		}

		protected override async Task ProcessAsync(GameIdArgs args, Result result)
		{
			await _db.Connection.ExecuteAsync("GameStatsInvolver", args);
		}

		public override async Task RunAsync()
		{
			var gameIds = _db.Connection
					.QuerySql<int>("SELECT Id FROM Games WHERE State<>1 AND HasStats = 0")
					.Select(GameIdArgs.Create)
					.ToList();

			_totalSteps = gameIds.Count;

			_currentStep = 0;

			var dt = DateTime.Now;

			await gameIds.ForEachAsync(THREADS, async item => await Round(item));

			var log = $"Итого: {(DateTime.Now - dt).Seconds}";

			Console.WriteLine(log);
			Log.Information(log);
		}

		public static void Run(Result total = null, params object[] arguments)
		{
			Task.Run(async () => await new GameStatsRunner(total).RunAsync(arguments)).Wait();
			//AsyncHelper.RunSync(async () => await new DayStatsRunner(total).RunAsync());
		}
	}
	*/
}