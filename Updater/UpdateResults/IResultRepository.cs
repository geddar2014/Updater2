using System.Collections.Generic;

namespace Updater.UpdateResults
{
	public interface IResultRepository
	{
		void Insert_Results(IList<Result> updateResults);
	}
}