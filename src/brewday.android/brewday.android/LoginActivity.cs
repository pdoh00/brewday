﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Refit;

namespace brewday.android
{
	[Activity (Label = "LoginActivity")]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Login);
			// Create your application here
			IBrewDayAuthApi authApi = RestService.For<IBrewDayAuthApi>("https://api.github.com");
			authApi.Register
		}
	}
}

