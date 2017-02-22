using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Threading;

namespace Vitality
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
			app.Screenshot("App Launched");
		}

		[Test]
		public void IncorrectLoginTest()
		{
			app.Tap("launcher_disclaimer_get_start");
			app.Screenshot("Let's start by Tapping on 'Get Started'");
			ScrollDownThroughEntireList();
			app.Tap("launcher_agreement_accept");
			app.Screenshot("Next we Tapped on the 'Agree' Button");

			Thread.Sleep(8000);
			app.EnterText("Vitality");
			app.Screenshot("Then we entered our username, 'Vitality'");
			app.DismissKeyboard();
			app.Screenshot("Dismissed Keyboard");
			app.Tap("launcher_user_password");
			app.Screenshot("We Tapped on the 'User Password' Text Field");
			app.EnterText("Microsoft");
			app.Screenshot("Then we entered our password, 'Microsoft'");
			app.DismissKeyboard();
			app.Screenshot("Dismissed Keyboard");

			app.Tap("launcher_login_login");
			app.Screenshot("Next we Tapped on the 'Login' Button");
			Thread.Sleep(8000);
			app.Tap("launcher_login_choose_variant");
			app.Screenshot("We chose to 'Log in Automatically'");
			Thread.Sleep(8000);
			app.Tap("launcher_login_submit_variant");
			app.Screenshot("Then we confirmed 'Auto Logging' option");
		}

		void ScrollDownThroughEntireList()
		{
			var scrollViewQuery = app.Query(x => x.Class("ScrollView")).FirstOrDefault();

			var startX = scrollViewQuery.Rect.CenterX;
			var endX = scrollViewQuery.Rect.CenterX;

			var startY = scrollViewQuery.Rect.Y + scrollViewQuery.Rect.Height - 10;
			var endY = scrollViewQuery.Rect.Y + 10;

			while (!IsAgreeButtonEnabled())
			{
				app.DragCoordinates(startX, startY, endX, endY);
			}
		}

		bool IsAgreeButtonEnabled()
		{
			var agreeButtonQuery = app.Query("launcher_agreement_accept")?.FirstOrDefault();

			return agreeButtonQuery?.Enabled ?? false;
		}

		[Test]
		public void SuccessfulLoginAndCheckRewardsTest()
		{
			app.Tap("launcher_disclaimer_get_start");
			app.Screenshot("Let's start by Tapping on 'Get Started'");
			ScrollDownThroughEntireList();
			app.Tap("launcher_agreement_accept");
			app.Screenshot("Next we Tapped on the 'Agree' Button");

			Thread.Sleep(8000);
			app.EnterText("eve.test51");
			app.Screenshot("Then we entered our username, 'eve.test51'");
			app.DismissKeyboard();
			app.Screenshot("Dismissed Keyboard");
			app.Tap("launcher_user_password");
			app.Screenshot("We Tapped on the 'User Password' Text Field");
			app.EnterText("P@ssw0rd");
			app.Screenshot("Then we entered our password, 'P@ssw0rd'");
			app.DismissKeyboard();
			app.Screenshot("Dismissed Keyboard");

			app.Tap("launcher_login_login");
			app.Screenshot("Next we Tapped on the 'Login' Button");
			Thread.Sleep(8000);
			app.Tap("launcher_login_choose_variant");
			app.Screenshot("We chose to 'Log in Automatically'");
			Thread.Sleep(8000);
			app.Tap("launcher_login_submit_variant");
			app.Screenshot("Then we confirmed 'Auto Logging' option");

			Thread.Sleep(8000);
			app.Tap(x => x.Class("android.widget.ImageButton"));
			app.Screenshot("We Tapped on the Hamburger Icon");

			app.Tap("Rewards");
			app.Screenshot("Then we Tapped on 'Rewards'");

			app.Tap("active_rewards_image");
			app.Screenshot("Next we Tapped on the 'Active Rewards' Image");

			app.Tap("active_rewards_learn_more");
			app.Screenshot("Then we Tapped on 'Learn More'");
		}

	}
}
