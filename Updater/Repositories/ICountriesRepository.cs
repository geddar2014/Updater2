using System.Collections.Generic;
using Updater.Apis.Dtos;

namespace Updater.Repositories
{
	public interface ICountriesRepository
	{
		void AddOrUpdate_Countries(IList<CountryDto> countriesInput, out int inserted, out int updated);
	}
}