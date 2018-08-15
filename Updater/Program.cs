using System;
using Serilog;
using Updater.Apis;
using Updater.Apis.Args;
using Updater.Apis.Runners;
using Updater.UpdateResults;

namespace Updater
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
					.WriteTo.File("update.log")
					.CreateLogger();

			var total = new Result(EmptyArgs.Create(), RunnerType.Full, "");

			//var stats = new SpanStatsSaver();

			//var shit = stats.GetSpanList(10787777);

			CountriesLeaguesRunner.Run(total);
			//
			SeasonsRunner.Run(total);
			//
			StagesTeamsRunner.Run(total);
			//
			//DayStatsRunner.Run(total);

			Console.WriteLine("END!!!");

			Console.ReadLine();
		}
	}
}