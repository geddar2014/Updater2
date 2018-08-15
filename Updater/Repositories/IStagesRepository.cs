using System.Collections.Generic;
using Updater.Apis.Dtos;

namespace Updater.Repositories
{
    public interface IStagesRepository
    {
        void AddOrUpdate_Stages(IList<StageDto> stagesInput, out int inserted, out int updated);
    }
}