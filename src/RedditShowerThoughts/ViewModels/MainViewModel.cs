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
using static RedditShowerThoughts.SimpleRedditService;

namespace RedditShowerThoughts
{
	public class MainViewModel : BaseViewModel
	{
        public Group Group { get; private set; }
        
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
        public string nextPageUrl { get; private set; }

        private async Task LoadItems()
		{
			var newItems = await SimpleRedditService.GetItems(Group, IsRefreshing, nextPageUrl);

            // get last entry
            nextPageUrl = newItems.Item2;

			if (IsRefreshing)
				Thoughts.Clear();

			foreach (var item in newItems.Item1)
			{
				Thoughts.Add(new RedditItemViewModel(item));
			}


		}

		public MainViewModel(Group group)
		{
            this.Group = group;
			Thoughts = new ObservableCollection<RedditItemViewModel>();
		}




	}
}
