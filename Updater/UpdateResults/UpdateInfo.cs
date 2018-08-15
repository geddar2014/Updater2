namespace Updater.UpdateResults
{
	public class UpdateInfo
	{
		public static string Compute(int currStep, int totalSteps, Result result, string disposition = null)
		{
			return
					$"# {disposition ?? $"{currStep}/{totalSteps}"} # ({currStep / (double) totalSteps * 100:0.0}%) # {result}";
		}
	}
}