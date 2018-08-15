using System.Collections.Generic;
using Updater.Apis.Dtos;

namespace Updater.Repositories
{
    public interface ISeasonsRepository
    {
        void AddOrUpdate_Seasons(IList<SeasonDto> seasonsInput, out int inserted, out int updated);
    }
}