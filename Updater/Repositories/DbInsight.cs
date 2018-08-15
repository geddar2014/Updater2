using System.Data;
using System.Data.SqlClient;
using Insight.Database;
using Updater.UpdateResults;

namespace Updater.Repositories
{
    public class DbInsight
    {
        private const string connStr =
//				"Server=mi3-wsq2.a2hosting.com;Database=betcahos_new;User ID=betcahos_sa;Password=Pa$$w0rd123";
            "Server=localhost; Database=betcahos_db; Trusted_Connection=True; User ID=sa; Password=Pa$$w0rd123;";

        public DbInsight()
        {
            SqlInsightDbProvider.RegisterProvider();

            Connection = new SqlConnection(connStr);

            CountriesRepository = Connection.AsParallel<ICountriesRepository>();
            LeaguesRepository   = Connection.AsParallel<ILeaguesRepository>();
            SeasonsRepository   = Connection.AsParallel<ISeasonsRepository>();
            StagesRepository    = Connection.AsParallel<IStagesRepository>();
            TeamsRepository     = Connection.AsParallel<ITeamsRepository>();

            //GameRepository      = Connection.AsParallel<IGameRepository>();
            //ResultRepository    = Connection.AsParallel<IResultRepository>();
            //SpanStatsRepository = Connection.AsParallel<ISpanStatsRepository>();
        }

        public IDbConnection        Connection          { get; }
        public ICountriesRepository CountriesRepository { get; }
        public ILeaguesRepository   LeaguesRepository   { get; }
        public ISeasonsRepository   SeasonsRepository   { get; }
        public IStagesRepository    StagesRepository    { get; }
        public ITeamsRepository     TeamsRepository     { get; }

        //public IGameRepository      GameRepository      { get; }
        //public IResultRepository    ResultRepository    { get; }
        //public ISpanStatsRepository SpanStatsRepository { get; }
    }
}