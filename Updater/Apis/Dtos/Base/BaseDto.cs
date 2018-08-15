using System.Text;
using Insight.Database;
using Newtonsoft.Json;
using Updater.Common;

namespace Updater.Apis.Dtos.Base
{
	public abstract class BaseDto : IDto
	{
		[RecordId]
        [JsonIgnore]
		public virtual string Id { get; set; }

		public override string ToString()
		{
			return $"[{Id}]";
		}

		public static string CalculateHash(params string[] parameters)
		{
		    StringBuilder sb = new StringBuilder();
			foreach (var parameter in parameters)
			{
				sb.Append(parameter);
			}
			return sb.ToString().ToMD5Hash();
		}
	}
}
