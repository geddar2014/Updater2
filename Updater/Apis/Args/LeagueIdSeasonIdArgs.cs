namespace Updater.Apis.Args
{
    public class LeagueIdSeasonIdArgs : IRunnerArgs
    {
        protected LeagueIdSeasonIdArgs()
        {
        }

        private LeagueIdSeasonIdArgs(string xLeagueId, string xSeasonId)
        {
            XLeagueId  = xLeagueId;
            XSeasonId  = xSeasonId;
        }


        public string XLeagueId { get; protected set; }

        public string XSeasonId { get; protected set; }

        public static IRunnerArgs Create(params object[] parameters)
        {
            return new LeagueIdSeasonIdArgs((string) parameters[0], (string) parameters[1]);
        }

    }
}