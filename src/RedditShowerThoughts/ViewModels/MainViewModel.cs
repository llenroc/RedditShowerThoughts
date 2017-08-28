using System;
using MvvmHelpers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;

namespace RedditShowerThoughts
{
	public class MainViewModel : BaseViewModel
	{
		public IList<RedditItemViewModel> Thoughts { get; private set; }

		private bool _isRefreshing = false;
		public bool IsRefreshing
		{
			get { return _isRefreshing; }
			set
			{
				_isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}

		public ICommand RefreshCommand
		{
			get
			{
				return new Command(async () =>
				{
					if (IsBusy) return;
					IsRefreshing = true;
					await LoadItems();
					IsRefreshing = false;
				});
			}
		}

		public ICommand LoadMoreItemsCommand
		{
			get
			{
				return new Command(async () =>
				{
					if (IsBusy || IsRefreshing) return;

					IsBusy = true;
					await LoadItems();
					IsBusy = false;
				});
			}
		}

		public bool HasMoreItemsToLoad { get; set; } = true;

		public int ItemsBeforeEndToLoad { get; set; } = 3;

		private async Task LoadItems()
		{
			var newItems = await SimpleRedditService.GetItems(IsRefreshing);

			if (IsRefreshing)
				Thoughts.Clear();

			foreach (var item in newItems)
			{
				Thoughts.Add(new RedditItemViewModel(item));
			}
		}

		public MainViewModel()
		{
			Thoughts = new ObservableCollection<RedditItemViewModel>();
		}




	}
}
