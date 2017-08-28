using Xamarin.Forms;

namespace RedditShowerThoughts
{
	public partial class App : Application
	{
		public static MainViewModel MainViewModel { get; private set; }

		public App()
		{
			InitializeComponent();
			MainViewModel = new MainViewModel();

			MainPage = new RedditShowerThoughtsPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
