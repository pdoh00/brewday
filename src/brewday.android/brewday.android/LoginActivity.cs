
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


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
//			IBrewDayAuthApi authApi = RestService.For<IBrewDayAuthApi>("https://api.github.com");
//			authApi.Register

			var loginBtn = FindViewById<Button>(Resource.Id.LoginButton);


			loginBtn.Click += async (object sender, EventArgs e) => {
				var prgDlg = new ProgressDialog(this);
				prgDlg.Indeterminate = true;
				prgDlg.SetProgressStyle(ProgressDialogStyle.Spinner);
				prgDlg.SetMessage("Logging in. Please wait...");
				prgDlg.SetCancelable(false);
				prgDlg.Show();
				await Login();
				prgDlg.Dismiss();
			};

		}

		async Task Login(){
			var emailEditText = FindViewById<Button>(Resource.Id.EmailEditText);
			var passwordEditText = FindViewById<Button>(Resource.Id.PasswordEditText);

			using (var httpClient = new HttpClient ()) {
				httpClient.BaseAddress = new Uri ("https://localhost:44303/");
				httpClient.DefaultRequestHeaders.Accept.Clear ();
				httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/x-www-form-urlencoded"));

				HttpResponseMessage result = null; 
				try
				{
					var content = new FormUrlEncodedContent(new[] 
						{
							new KeyValuePair<string, string>("grant_type", "password"),
							new KeyValuePair<string, string>("username", emailEditText.Text),
							new KeyValuePair<string, string>("password", passwordEditText.Text),
						});
					result = await httpClient.PostAsync("login", content);
				}
				catch(System.Net.WebException) {
					var toast = new Toast (this);
					toast.SetText ("Server unavailble");
					toast.Duration = ToastLength.Long;
					toast.Show ();
				}

				if (result.IsSuccessStatusCode) {
					//TODO: get token. store local
					var token = await result.Content.ReadAsStringAsync ();
					StartActivity (typeof(MainActivity));
				} else {
					var toast = new Toast (this);
					toast.SetText ("Login failed.");
					toast.Duration = ToastLength.Long;
					toast.Show ();
				}
				
			}
		}
	}
}

