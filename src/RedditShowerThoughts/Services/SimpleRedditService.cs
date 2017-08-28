using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RedditShowerThoughts
{
	public static class SimpleRedditService
	{
		static HttpClient httpClient;
		private static string baseUrl = @"http://www.reddit.com";
		private static string after = "";
		private static string subredditGroup = "new";
//		private static string subredditGroup = "top";
		private static string subreddit = "showerthoughts";
		static SimpleRedditService()
		{
			httpClient = new HttpClient();
		}

		public static async Task<List<RedditItem>> GetItems(bool reload)
		{
			// construct url
			string url = $"{baseUrl}/r/{subreddit}/{subredditGroup}.json?raw_json=1";

			if (reload == false && !string.IsNullOrEmpty(after))
				url += "&after=" + after;

			// get the data
			var result = await httpClient.GetStringAsync(url);
			var resultValue = JsonConvert.DeserializeObject<RootObject>(result);

			// save pointer to next "page"
			after = resultValue.data.after;

			// create data to return
			List<RedditItem> returnData = new List<RedditItem>();

			foreach (var item in resultValue.data.children)
				returnData.Add(item.data);

			return returnData;
		}
	}
}
