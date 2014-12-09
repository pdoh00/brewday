using System;
using System.Threading.Tasks;
using Refit;

namespace brewday.android
{
	//This is not going to work for Hypermedia.
	//Let's just get it working with this and then move to hyper and 
	//dynamic layout based on rel

	public interface IBrewDayAuthApi
	{
		[Post("api/account/register")]
		Task Register(string user);
		[Post("api/account/login")]
		Task Register([Body(BodySerializationMethod.UrlEncoded)] Login login);
	}
}

