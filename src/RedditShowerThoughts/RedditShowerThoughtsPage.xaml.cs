using Xamarin.Forms;
using System.Diagnostics;
using System.Collections;

namespace RedditShowerThoughts
{
	public partial class RedditShowerThoughtsPage : ContentPage
	{
		MainViewModel viewModel;
		
		public RedditShowerThoughtsPage()
		{
			InitializeComponent();
			this.viewModel = App.MainViewModel;
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
