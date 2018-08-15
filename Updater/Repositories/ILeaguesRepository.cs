using System.Collections.Generic;
using Updater.Apis.Dtos;

namespace Updater.Repositories
{
	public interface ILeaguesRepository
	{
		void AddOrUpdate_Leagues(IList<LeagueDto> leaguesInput, out int inserted, out int updated);
	}
}