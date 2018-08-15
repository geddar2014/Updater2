using System.Linq;
using System.Threading.Tasks;
using Updater.Apis.Args;
using Updater.Apis.Dtos;
using Updater.Apis.Dtos.Base;
using Updater.Common;
using Updater.UpdateResults;

namespace Updater.Apis.Runners
{
    public class CountriesLeaguesRunner : Runner<EmptyArgs, GetCountriesLeaguesOutput>
    {
        protected CountriesLeaguesRunner(Result total = null) : base(total)
        {
            _total = total ?? new Result(EmptyArgs.Create(), RunnerType.CountriesLeagues);
        }

        protected override async Task ProcessAsync(EmptyArgs args, Result result)
        {
            var countries = (await GetAsync("component_data/2/3-0-0-0-0-0-0"))
                           .OutStats
                           .Countries
                           .Select(c =>
                            {
                                c.XCountryId = c.XCountryId;
                                c.Id = BaseDto.CalculateHash(c.XCountryId);
                                return c;
                            })
                           .ToList();

            if (countries.Count > 0)
            {
                _db.CountriesRepository.AddOrUpdate_Countries(
                                                              countries,
                                                              out var countriesInserted,
                                                              out var countriesUpdated);

                result.CountriesInserted = countriesInserted;
                result.CountriesUpdated  = countriesUpdated;
            }

            var leagues = countries
                         .SelectMany(c => c.Leagues.Select(l =>
                          {
                              l.XCountryId = c.XCountryId;
                              l.XLeagueId = l.XLeagueId;
                              l.ParentId = BaseDto.CalculateHash(l.XCountryId);
                              l.Id = BaseDto.CalculateHash(l.XCountryId, l.XLeagueId);
                              return l;
                          }))
                         .ToList();

            if (leagues.Count > 0)
            {
                _db.LeaguesRepository.AddOrUpdate_Leagues(
                                                          leagues,
                                                          out var leaguesInserted,
                                                          out var leaguesUpdated);

                result.LeaguesInserted = leaguesInserted;
                result.LeaguesUpdated  = leaguesUpdated;
            }
        }

        public override async Task RunAsync()
        {
            _totalSteps = 1;
            await Round(EmptyArgs.Create());
        }


        public static void Run(Result total = null)
        {
            Task.Run(async () => await new CountriesLeaguesRunner(total).RunAsync()).Wait();
        }
    }
}