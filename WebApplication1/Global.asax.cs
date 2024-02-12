using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Autofac;
using WebApplication1.Repository.IRepository;
using WebApplication1.Repository.Repositories;
using WebApplication1.Service.IService;
using WebApplication1.Service.Services;
using WebApplication1.Controllers;
using Autofac.Integration.Mvc;

namespace WebApplication1
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();
            builder.RegisterType<PesaPalRepository>().As<IPesaPalRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PeopleRepository>().As<IPeopleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PesaPalService>().As<IPesaPalService>().InstancePerLifetimeScope();
            builder.RegisterType<PeopleController>().As<PeopleController>().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}