using Xamarin.Forms;
using System.Diagnostics;
using System.Collections;
using static RedditShowerThoughts.SimpleRedditService;

namespace RedditShowerThoughts
{
	public partial class RedditShowerThoughtsPage : ContentPage
	{
		MainViewModel viewModel;
		
		public RedditShowerThoughtsPage(Group group)
		{
			InitializeComponent();
			this.viewModel = new MainViewModel(group);
			this.BindingContext = viewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			mainList.ItemAppearing += MainList_ItemAppearing;
			viewModel.RefreshCommand.Execute(null);
		}

		void MainList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			if (!viewModel.HasMoreItemsToLoad) return;

			var items = mainList.ItemsSource as IList;
			if (items != null && 
			    e.Item == items[items.Count - viewModel.ItemsBeforeEndToLoad])
			{
				viewModel.LoadMoreItemsCommand.Execute(null);
			}
		}

	}
}
