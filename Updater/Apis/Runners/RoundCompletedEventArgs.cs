using System;
using Updater.UpdateResults;

namespace Updater.Apis.Runners
{
	public class RoundCompletedEventArgs : EventArgs
	{
		public RoundCompletedEventArgs(Result result)
		{
			Result = result;
		}

		public Result Result { get; }
	}
}