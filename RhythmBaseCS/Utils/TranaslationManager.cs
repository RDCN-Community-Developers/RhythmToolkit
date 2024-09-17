using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace RhythmBase.Utils
{
	/// <summary>
	/// Provides methods for creating and reading property names.
	/// </summary>
	public class TranaslationManager
	{
		public TranaslationManager(FileInfo filepath)
		{
			jsonpath = filepath;
			bool exists = jsonpath.Exists;
			if (!exists)
			{
				jsonpath.Directory.Create();
				using (StreamWriter stream = new(jsonpath.Create()))
				{
					stream.Write("{}");
				}
			}
			using (StreamReader Stream = new(jsonpath.OpenRead()))
			{
				values = (JObject)JsonConvert.DeserializeObject(Stream.ReadToEnd());
			}
		}

		public string GetValue(MemberInfo p, string value)
		{
			JObject current = values;
			string[] keys = GetPath(p);
			checked
			{
				int num = keys.Length - 2;
				for (int i = 0; i <= num; i++)
				{
					JToken j = null;
					bool flag = !current.TryGetValue(keys[i], out j);
					if (flag)
					{
						current[keys[i]] = new JObject();
						current = (JObject)current[keys[i]];
					}
					else
					{
						current = (JObject)j;
					}
				}
				bool flag2 = !current.ContainsKey(keys.Last()) || current[keys.Last()] == null;
				string GetValue;
				if (flag2)
				{
					current[keys.Last()] = value;
					Save();
					GetValue = value;
				}
				else
				{
					GetValue = current[keys.Last()].ToString();
				}
				return GetValue;
			}
		}

		public object GetValue(MemberInfo p) => GetValue(p, GetPath(p).Last());

		private static string[] GetPath(MemberInfo p) =>
			[
				p.DeclaringType.Namespace,
				p.DeclaringType.Name,
				p.Name
			];

		private void Save()
		{
			using (StreamWriter Stream = new(jsonpath.OpenWrite()))
			{
				Stream.Write(values.ToString());
			}
		}

		private readonly FileInfo jsonpath;

		private readonly JObject values;
	}
}
