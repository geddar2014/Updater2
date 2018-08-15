//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Insight.Database;
//using Newtonsoft.Json;
//using Updater.Apis;
//using Updater.Apis.Args;
//using Updater.Apis.Dtos;
//using Updater.Common;
//using Updater.UpdateResults;

//namespace Updater.Runners
//{
//	public class LeaguesRunner : Runner<CountryIdArgs, IList<GetLeaguesOutput>>
//	{
//		protected LeaguesRunner(Result total = null) : base(total)
//		{
//			_total = total ?? new Result(EmptyArgs.Create(), RunnerType.Leagues);
//		}

//		protected override async Task ProcessAsync(CountryIdArgs args, Result result)
//		{
//			var l = await GetAsync($"Sport/3/Category/{args.CountryId}/Champ?ln=ru");

//			var leagues = l.Select(x =>
//			{
//				x.League.CountryId = args.CountryId;
//				return x.League;
//			}).ToList();

//			if (leagues.Count > 0)
//			{
//				_db.LeagueRepository.AddOrUpdate_Leagues(leagues, out var leaguesInserted,
//						out var leaguesUpdated);
//				result.LeaguesInserted = leaguesInserted;
//				result.LeaguesUpdated  = leaguesUpdated;
//			}
//		}

//		public override async Task RunAsync()
//		{
//			var updatedCountryIds =
//					_db.Connection
//							.QuerySql<string>(
//									$"SELECT Args FROM UpdateResults WHERE RunnerType = {(int) RunnerType.Leagues}")
//							.Distinct()
//							.Select(JsonConvert.DeserializeObject<CountryIdArgs>)
//							.Select(x => x.CountryId)
//							.OrderBy(x => x);

//			var allCountryIds = _db.Connection.QuerySql<int>("SELECT Id FROM Countries").OrderBy(x => x);

//			var unfetchedCountryIds = allCountryIds
//					//.Except(updatedCountryIds)
//					.Select(CountryIdArgs.Create).ToList();

//			_totalSteps = unfetchedCountryIds.Count;

//			_currentStep = 0;
//			var dt = DateTime.Now;
//			await unfetchedCountryIds.ForEachAsync(THREADS, async item => await Round(item));
//			var log = $"{THREADS} threads: {(DateTime.Now - dt).Seconds}";
//			Console.WriteLine(log);

//			//for (var i = 0; i < unfetchedCountryIds.Count; i++)
//			//	await Round(unfetchedCountryIds[i]);
//		}

//		public static void Run(Result total = null)
//		{
//			Task.Run(async () => await new LeaguesRunner(total).RunAsync()).Wait();
//			//AsyncHelper.RunSync(async () => await new LeaguesRunner(total).RunAsync());
//		}
//	}
//}