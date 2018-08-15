using System.Collections.Generic;
using Updater.Apis.Dtos;

namespace Updater.Repositories
{
	public interface ITeamsRepository
	{
		void AddOrUpdate_Teams(IList<TeamDto> teamsInput, out int inserted, out int updated);
	}
}