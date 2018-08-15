using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable InconsistentNaming

namespace Updater.Common
{
	public static class DirectoryUtils
	{
		public static string GetApplicationRoot()
		{
			var exePath = Path.GetDirectoryName(System.Reflection
				.Assembly.GetExecutingAssembly().CodeBase);
			Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
			var appRoot = appPathMatcher.Match(exePath).Value;
			return appRoot;
		}

		public static string ToApplicationPath(this string fileName)
		{
			var exePath = Path.GetDirectoryName(System.Reflection
				.Assembly.GetExecutingAssembly().CodeBase);
			Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
			var appRoot = appPathMatcher.Match(exePath).Value;
			return Path.Combine(appRoot, fileName);
		}

		public static string Base64Encode(this string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}

		public static string Base64Decode(this string base64EncodedData)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}

		public static string ToMD5Hash(this string input) => input.ToByteArray().ToMD5Hash();

		public static string ToMD5Hash(this byte[] input)
		{
			var md5 = MD5.Create();

			byte[] hash = md5.ComputeHash(input);

			return hash.ToHexString();
		}

		public static string ToHexString(this byte[] input)
		{
			var sb = new StringBuilder();

			foreach (var t in input)
			{
				sb.Append(t.ToString("x2"));
			}

			return sb.ToString();
		}

		public static byte[] ToByteArray(this object obj)
		{
			if (obj == null)
				return null;
			var bf = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}
	}
}