namespace Updater.Apis.Args
{
	public class CountryIdLeagueIdArgs : IRunnerArgs
	{
		protected CountryIdLeagueIdArgs()
		{
		}

		private CountryIdLeagueIdArgs(string xCountryId, string xLeagueId)
		{
		    XCountryId = xCountryId;
			XLeagueId  = xLeagueId;
		}
		
	    public string XCountryId { get; protected set; }

		public string XLeagueId  { get; protected set; }
		
		public static IRunnerArgs Create(string xCountryId, string xLeagueId)
		{
			return new CountryIdLeagueIdArgs(xCountryId,xLeagueId);
		}
	}
}