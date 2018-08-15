using System;
using System.Linq;
using System.Threading.Tasks;
using Insight.Database;
using Serilog;
using Updater.Apis.Args;
using Updater.Apis.Dtos;
using Updater.Apis.Dtos.Base;
using Updater.Common;
using Updater.UpdateResults;

namespace Updater.Apis.Runners
{
	public class SeasonsRunner : Runner<CountryIdLeagueIdArgs, GetSeasonsOutput>
	{
		protected SeasonsRunner(Result total = null) : base(total)
		{
			_total = total ?? new Result(EmptyArgs.Create(), RunnerType.Seasons);
		}

		protected override async Task ProcessAsync(CountryIdLeagueIdArgs args, Result result)
		{
			var seasonsUri = $"component_data/3/3-{args.XLeagueId}-0-0-0-0-0";

			var seasons = (await GetAsync(seasonsUri)).OutStats.Seasons.Select(s =>
			{
				s.XCountryId = args.XCountryId;
				s.XLeagueId = args.XLeagueId;
				s.XSeasonId = s.XSeasonId;
				s.ParentId = BaseDto.CalculateHash(s.XCountryId, s.XLeagueId);
				s.Id = BaseDto.CalculateHash(s.XCountryId, s.XLeagueId, s.XSeasonId);
				return s;
			}).ToList();

			if (seasons.Count > 0)
			{
				_db.SeasonsRepository.AddOrUpdate_Seasons(seasons, out var seasonsInserted, out var seasonsUpdated);
				result.SeasonsInserted = seasonsInserted;
				result.SeasonsUpdated = seasonsUpdated;
			}
		}

		public override async Task RunAsync()
		{
			//var updatedCountryIdLeagueIds = _db.Connection
			//		.QuerySql<string>(
			//				$"SELECT Args FROM UpdateResults WHERE RunnerType = {(int) RunnerType.Seasons}")
			//		.Distinct()
			//		.Select(JsonConvert.DeserializeObject<CountryIdLeagueIdArgs>)
			//		.OrderBy(x => x.CountryId)
			//		.ThenBy(x => x.LeagueId);

			var query = _db.Connection
						   .QuerySql<LeagueDto>("SELECT XCountryId, XLeagueId  FROM Leagues");

			var allLeagueIds = query
							  .Select(x => CountryIdLeagueIdArgs.Create(x.XCountryId, x.XLeagueId))
							  .OrderBy(x => ((CountryIdLeagueIdArgs)x).XLeagueId);

			var unfetchedCountryIdLeagueIds =
					allLeagueIds
						   //.Except(updatedCountryIdLeagueIds)
						   .ToList();

			_totalSteps = unfetchedCountryIdLeagueIds.Count;

			_currentStep = 0;
			var dt = DateTime.Now;

			await unfetchedCountryIdLeagueIds.ForEachAsync(THREADS, async item => await Round((CountryIdLeagueIdArgs)item));

			var log = $"{THREADS} threads: {(DateTime.Now - dt).Seconds}";

			Console.WriteLine(log);
			Log.Information(log);
		}

		public static void Run(Result total = null)
		{
			Task.Run(async () => await new SeasonsRunner(total).RunAsync()).Wait();
		}
	}
}