
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
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

				var emailEditText = FindViewById<EditText>(Resource.Id.EmailEditText);
				var passwordEditText = FindViewById<EditText>(Resource.Id.PasswordEditText);

				await Login(emailEditText.Text, passwordEditText.Text);
				prgDlg.Dismiss();
			};

		}

		async Task Login(string email, string password){


//			HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://localhost:44303/login");
//
//			req.Method = "POST";
//			req.ContentType = "application/x-www-form-urlencoded";         
//			req.ContentLength = 6;
//			var strRequest = string.Format ("grant_type=password&username={0}&password={1}", email, password);
//			var streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
//			streamOut.Write(strRequest);
//			streamOut.Close();
//			var streamIn = new StreamReader(req.GetResponse().GetResponseStream());
//			string strResponse = streamIn.ReadToEnd();
//			streamIn.Close();
//
//			req.BeginGetResponse ((ar) => {
//				var request = (HttpWebRequest)ar.AsyncState;
//				using (var response = (HttpWebResponse)request.EndGetResponse (ar)){ 
//					var s = response.GetResponseStream();
//					Console.WriteLine(s);
//				}
//			}, req);

//			if (result.IsSuccessStatusCode) {
//				//TODO: get token. store local
//				var token = await result.Content.ReadAsStringAsync ();
//				StartActivity (typeof(MainActivity));
//			} else {
//				var toast = new Toast (this);
//				toast.SetText ("Login failed.");
//				toast.Duration = ToastLength.Long;
//				toast.Show ();
//			}

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
							new KeyValuePair<string, string>("username", email),
							new KeyValuePair<string, string>("password", password),
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

