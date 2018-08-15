using Newtonsoft.Json;
using Updater.Common;

namespace Updater.Apis.Dtos.Base
{
    public abstract class BaseDtoWithTitle : BaseDto, IDtoWithTitle
    {
        public virtual string Title { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Title}";
        }
    }
}
