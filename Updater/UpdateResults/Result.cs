using System;
using Newtonsoft.Json;
using Updater.Apis;
using Updater.Apis.Args;

namespace Updater.UpdateResults
{
    public class Result
    {
        public string Args;

        public Result(IRunnerArgs args)
        {
            Started    = DateTime.Now;
            RunnerArgs = args;
            switch (args)
            {
                case EmptyArgs e:
                    RunnerType  = RunnerType.CountriesLeagues;
                    Disposition = $"";
                    break;
                //case CountryIdArgs c:
                //	RunnerType  = RunnerType.Leagues;
                //	Disposition = $"C:{c.CountryId}";
                //	break;
                case CountryIdLeagueIdArgs c:
                    RunnerType  = RunnerType.Seasons;
                    Disposition = $"C:{c.XCountryId} L:{c.XLeagueId}";
                    break;
                case LeagueIdSeasonIdArgs c:
                    RunnerType  = RunnerType.StagesTeams;
                    Disposition = $"C:n|a L:{c.XLeagueId} S:{c.XSeasonId}";
                    break;
                //case DateArgs d:
                //	RunnerType  = RunnerType.DayStats;
                //	Disposition = $"{d.Date.ToShortDateString()}";
                //	break;
            }
        }

        public Result(IRunnerArgs args, RunnerType type) : this(args)
        {
            RunnerType = type;
        }

        public Result(IRunnerArgs args, RunnerType type, string disposition) : this(args, type)
        {
            Disposition = disposition;
        }

        public IRunnerArgs RunnerArgs
        {
            get => JsonConvert.DeserializeObject<IRunnerArgs>(Args);
            set => Args = JsonConvert.SerializeObject(value);
        }

        public RunnerType RunnerType { get; set; }

        public int      Id                { get; set; }
        public string   Disposition       { get; set; }
        public int      CountriesInserted { get; set; }
        public int      CountriesUpdated  { get; set; }
        public int      LeaguesInserted   { get; set; }
        public int      LeaguesUpdated    { get; set; }
        public int      SeasonsInserted   { get; set; }
        public int      SeasonsUpdated    { get; set; }
        public int      StagesInserted    { get; set; }
        public int      StagesUpdated     { get; set; }
        public int      TeamsInserted     { get; set; }
        public int      TeamsUpdated      { get; set; }
        public int      GamesInserted     { get; set; }
        public int      GamesUpdated      { get; set; }
        public DateTime Started           { get; set; }
        public DateTime Ended             { get; set; }
        public TimeSpan Elapsed           => DateTime.Now - Started;

        public void Complete()
        {
            Ended = DateTime.Now;
        }


        public Result Append(Result another)
        {
            CountriesInserted += another.CountriesInserted;
            CountriesUpdated  += another.CountriesUpdated;
            LeaguesInserted   += another.LeaguesInserted;
            LeaguesUpdated    += another.LeaguesUpdated;
            SeasonsInserted   += another.SeasonsInserted;
            SeasonsUpdated    += another.SeasonsUpdated;
            StagesInserted    += another.StagesInserted;
            StagesUpdated     += another.StagesUpdated;
            TeamsInserted     += another.TeamsInserted;
            TeamsUpdated      += another.TeamsUpdated;
            GamesInserted     += another.GamesInserted;
            GamesUpdated      += another.GamesUpdated;

            return this;
        }

        public override string ToString()
        {
            return
                $"{Elapsed:%h\\:mm\\:ss} # I( {CountriesInserted} | {LeaguesInserted} | {SeasonsInserted} | {StagesInserted} | {TeamsInserted} | {GamesInserted} ) U( {CountriesUpdated} | {LeaguesUpdated} | {SeasonsUpdated} | {StagesUpdated} | {TeamsUpdated} | {GamesUpdated} )";
        }
    }
}