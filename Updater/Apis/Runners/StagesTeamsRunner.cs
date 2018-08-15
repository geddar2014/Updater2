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
    public class StagesTeamsRunner : Runner<LeagueIdSeasonIdArgs, GetStagesTeamsOutput>
    {
        protected StagesTeamsRunner(Result total = null) : base(total)
        {
            _total = total ?? new Result(EmptyArgs.Create(), RunnerType.Seasons);
        }

        protected override async Task ProcessAsync(LeagueIdSeasonIdArgs args, Result result)
        {
            var stagesTeamsUri = $"component_data/3/3-{args.XLeagueId}-{args.XSeasonId}-0-0-0-0";

            var outStats = (await GetAsync(stagesTeamsUri)).OutStats;

            var stages = outStats.Stages.Select(s =>
            {
                s.XLeagueId = args.XLeagueId;
                s.XSeasonId = args.XSeasonId;
                s.ParentId = BaseDto.CalculateHash(s.XCountryId, s.XLeagueId, s.XSeasonId);
                s.Id = BaseDto.CalculateHash(s.XCountryId, s.XLeagueId, s.XSeasonId, s.XStageId);
                return s;
            }).ToList();

            if (stages.Count > 0)
            {
                var stm = stages.Select(x => (StageDto)x).ToList();

                _db.StagesRepository.AddOrUpdate_Stages(stm, out var stagesInserted, out var stagesUpdated);
                result.StagesInserted = stagesInserted;
                result.StagesUpdated = stagesUpdated;
            }

            #region Teams

            var a = stages.Where(s => s.A != null && s.A.C.Count > 0)
                          .SelectMany(s => s.A.C)
                          .Where(x => x?.R != null && x.R.Count > 0)
                          .SelectMany(x => x.R)
                          .Where(x => x.T != null)
                          .Select(x => x.T);
            var d = stages.Where(s => s.D != null && s.D.C.Count > 0)
                          .SelectMany(s => s.D.C)
                          .Where(x => x != null && x.A != null && x.H != null)
                          .SelectMany(x => new[] { x.A, x.H });
            var teams = a.Union(d)
                         .GroupBy(g => g.XTeamId)
                         .Select(g => g.First())
                         .Select(x =>
                          {
                              x.Id = BaseDto.CalculateHash(x.XTeamId);
                              return x;
                          })
                         .ToList();

            if (teams.Count > 0)
            {
                _db.TeamsRepository.AddOrUpdate_Teams(teams, out var teamsInserted, out var teamsUpdated);
                result.TeamsInserted = teamsInserted;
                result.TeamsUpdated = teamsUpdated;
            }

            #endregion

            //#region countries
            //
            //    var countries = dto.Teams.Select(x => x.CountryId).Distinct().Select(x => new CountryDto(x))
            //                       .ToList();
            //
            //    if (countries.Count > 0)
            //    {
            //        _db.CountryRepository.AddIfNotExists_Countries(countries, out var countriesInserted);
            //        result.CountriesInserted = countriesInserted;
            //    }
            //
            //#endregion
            //
            //#region stages
            //
            //    var stages = dto.Stages;
            //
            //    if (stages.Count > 0)
            //    {
            //        _db.StageRepository.AddOrUpdate_Stages(stages, out var stagesInserted, out var stagesUpdated);
            //        result.StagesInserted = stagesInserted;
            //        result.StagesUpdated  = stagesUpdated;
            //    }
            //
            //#endregion
            //
            //
            //#region Games
            //
            //    var games = dto.Games;
            //
            //    if (games.Count > 0)
            //    {
            //        _db.GameRepository.AddOrUpdate_Games(games, out var gamesInserted, out var gamesUpdated);
            //        result.GamesInserted = gamesInserted;
            //        result.GamesUpdated  = gamesUpdated;
            //    }
            //
            //#endregion
        }


        public override async Task RunAsync()
        {
            //var updatedCountryIdLeagueIdSeasonIds = _db.Connection
            //                                           .QuerySql<string>(
            //                                                             $"SELECT Args FROM UpdateResults WHERE RunnerType = {(int) RunnerType.StagesTeams}")
            //                                           .Distinct()
            //                                           .Select(JsonConvert
            //                                                      .DeserializeObject<CountryIdLeagueIdSeasonIdArgs>)
            //                                           .OrderBy(x => x.CountryId).ThenBy(x => x.LeagueId)
            //                                           .ThenBy(x => x.SeasonId).ToList();
            //
            var query = _db.Connection
                           .QuerySql<SeasonDto>("SELECT * FROM Seasons");

            var allLeagueIdSeasonIds = query
                                      .Select(x => ((LeagueIdSeasonIdArgs)LeagueIdSeasonIdArgs.Create(x.XLeagueId,
                                                                                                       x.XSeasonId)))
                                      .OrderBy(x => x.XLeagueId).ThenBy(x => x.XSeasonId).ToList();

            var unfetchedLeagueIdSeasonIds =
                allLeagueIdSeasonIds
                   //.Except(updatedCountryIdLeagueIdSeasonIds)
                   .ToList();

            _totalSteps = unfetchedLeagueIdSeasonIds.Count;

            _currentStep = 0;
            var dt = DateTime.Now;

            await unfetchedLeagueIdSeasonIds.ForEachAsync(THREADS,
                                                          async item => await Round((LeagueIdSeasonIdArgs)item));

            var log = $"{THREADS} threads: {(DateTime.Now - dt).Seconds}";

            Console.WriteLine(log);
            Log.Information(log);
        }

        public static void Run(Result total = null)
        {
            Task.Run(async () => await new StagesTeamsRunner(total).RunAsync()).Wait();
        }
    }
}