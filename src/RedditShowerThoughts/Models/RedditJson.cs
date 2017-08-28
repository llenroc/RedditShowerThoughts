using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RedditShowerThoughts
{
	[JsonObject(Title = "Data2")]
	public class RedditItem
	{
		public string id { get; set; }
		public string author { get; set; }
		public string name { get; set; }
		public string subreddit_name_prefixed { get; set; }
		public bool over_18 { get; set; }
		public string thumbnail { get; set; }
		public string subreddit_id { get; set; }
		public double created { get; set; }
		public string url { get; set; }
		public string title { get; set; }
		public double created_utc { get; set; }
	}

	public class Child
	{
		public string kind { get; set; }
		public RedditItem data { get; set; }
	}

	public class Data
	{
		public string modhash { get; set; }
		public List<Child> children { get; set; }
		public string after { get; set; }
		public object before { get; set; }
	}

	public class RootObject
	{
		public string kind { get; set; }
		public Data data { get; set; }
	}
}
