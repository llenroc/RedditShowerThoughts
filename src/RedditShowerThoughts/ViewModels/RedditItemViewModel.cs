using MvvmHelpers;
namespace RedditShowerThoughts
{
	public class RedditItemViewModel : BaseViewModel
	{
		readonly RedditItem reditItem;

		public RedditItemViewModel(RedditItem item)
		{
			this.reditItem = item;
		}

		public string ShowerThought
		{
			get { return reditItem.title; }
		}

		public string Author
		{
			get { return reditItem.author; }
		}
	}
}
