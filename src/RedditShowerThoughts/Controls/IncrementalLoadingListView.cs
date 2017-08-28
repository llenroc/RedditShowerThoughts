using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
namespace RedditShowerThoughts
{
	// FORGET ABOUT THIS CLASS AT THE MOMENT -- FIGURED I SHOULD ENCAPSULATE IN A LISTVIEW
	public class IncrementalLoadingListView : ListView
	{

		public ICommand LoadMoreCommand
		{
			get { return (ICommand)GetValue(LoadMoreCommandProperty); }
			set { SetValue(LoadMoreCommandProperty, value); }
		}

		public static readonly BindableProperty LoadMoreCommandProperty =
  				BindableProperty.Create(
				propertyName: "LoadMoreCommand",
				returnType: typeof(ICommand),
				declaringType: typeof(IncrementalLoadingListView),
				defaultBindingMode: BindingMode.TwoWay);

		public bool SourceHasMoreItems
		{
			get { return (bool)GetValue(SourceHasMoreItemsProperty); }
			set { SetValue(SourceHasMoreItemsProperty, value); }
		}

		public static readonly BindableProperty SourceHasMoreItemsProperty =
		  BindableProperty.Create(
		propertyName: "SourceHasMoreItems",
		returnType: typeof(bool),
		declaringType: typeof(IncrementalLoadingListView),
		defaultBindingMode: BindingMode.TwoWay);


		public IncrementalLoadingListView()
		{
			this.ItemAppearing += MainList_ItemAppearing;
		}

		void MainList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			var items = this.ItemsSource as IList;

			// check if we are at the last item list item
			if (items != null && e.Item == items[items.Count - 1])
			{
				// if the loadmore command is setup and available, load more
				if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
					LoadMoreCommand.Execute(null);
			}
		}

	}
}
