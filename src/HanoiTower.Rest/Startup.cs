using System.IO;
using System.Web.Http;
using Hangfire;
using Hangfire.SimpleInjector;
using Hanoi.Data.Contexts;
using Hanoi.Data.Repositories;
using HanoiTower.Domain.Commands.Handlers;
using HanoiTower.Domain.Interfaces.Repositories;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace HanoiTower.Rest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigRoutes(config);
            ConfigureIoC(config);
            log4net.Config.XmlConfigurator.Configure();

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Data Source=DESKTOP-GTMD0BG\SQLEXPRESS;Initial Catalog=hangfire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public static void ConfigRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new LogRequestResponseHandler());
        }

        public static void ConfigureIoC(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<HanoiDataContext>(Lifestyle.Scoped);
            container.Register<IHanoiRepository, HanoiRepository>(Lifestyle.Scoped);
            container.Register<IMoveRepository, MoveRepository>(Lifestyle.Scoped);
            container.Register<HanoiCommandsHandler>(Lifestyle.Scoped);
            container.Register<MoveCommandsHandler>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.UseActivator(new SimpleInjectorJobActivator(container));
        }
    }
}