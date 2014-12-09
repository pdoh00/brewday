
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace brewday.android
{
	//NoHistory - tell Android not to put the activity in the 'back stack', that is, when the
	//user hits the back button from the real application, don't show this activity again
	[Activity (Theme="@style/Theme.Splash", MainLauncher=true, NoHistory=true)]			
	public class SplashActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Thread.Sleep (10000);
			StartActivity (typeof(MainActivity));
		}
	}
}

