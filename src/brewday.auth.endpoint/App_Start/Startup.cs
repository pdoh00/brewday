using brewday.auth.endpoint.Models;
using brewday.auth.endpoint.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;
using System;
using System.Web.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(brewday.auth.endpoint.Startup))]

namespace brewday.auth.endpoint
{
    public class Startup
    {
        public IKernel Kernal { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();

            var csConfig = WebConfigurationManager.OpenWebConfiguration("/brewday.auth.endpoint");
            var connStr = csConfig.ConnectionStrings.ConnectionStrings["Identity"];

            var connectionString = AuthenticationDatabase.LocalizeSQLiteConnection(connStr);
            AuthenticationDatabase.InitializeSQLiteDatabase(connectionString);

            Kernal = CreateKernel(connectionString);
            
            config.DependencyResolver = new NinjectDependencyResolver(Kernal);

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
#if DEBUG
                AllowInsecureHttp = true,
#else
                AllowInsecureHttp = false,
#endif
                TokenEndpointPath = new PathString("/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider(Kernal.Get<IAuthRepository>())
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public static IKernel CreateKernel(string authDatabaseConnectionString)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IUserStore<User>>()
                .To<SqliteUserStore<User>>()
                .WithConstructorArgument("connectionString", authDatabaseConnectionString);

            kernel.Bind<UserManager<User>>()
                .ToSelf();

            kernel.Bind<IAuthRepository>()
                .To<SqliteAuthRepository>();

            return kernel;
        }
    }
}
