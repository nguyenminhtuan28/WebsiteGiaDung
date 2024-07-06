using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
namespace WEBPROJECT.Repository
{
	public static class SessionExtensions
	{
		 public static void SetJson(this ISession session , string key,object value)
		{
			session.SetString(key,JsonConvert.SerializeObject(value));
		}
		public static T GetJson<T> (this ISession session , string key)
		{
			var SessionData=session.GetString(key);
			return SessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(SessionData);
		}
	}
}
