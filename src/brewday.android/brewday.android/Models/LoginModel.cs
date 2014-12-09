using System;
using System.Threading.Tasks;
using Refit;

namespace brewday.android
{
	public class LoginModel
	{
		[AliasAs("grant_type")]
		public string GrantType {
			get;
			set;
		}

		[AliasAs("username")]
		public string UserName {
			get;
			set;
		}

		[AliasAs("password")]
		public string Password {
			get;
			set;
		}
	}

}

