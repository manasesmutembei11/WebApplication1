using Autofac;
using Autofac.Core;
using WebApplication1.Repository.IRepository;
using WebApplication1.Repository.Repositories;
using WebApplication1.Service.IService;
using WebApplication1.Service.Services;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register repositories
        builder.RegisterType<PesaPalRepository>().As<IPesaPalRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PeopleRepository>().As<IPeopleRepository>().InstancePerLifetimeScope();

        // Register services
        builder.RegisterType<PesaPalService>().As<IPesaPalService>().InstancePerLifetimeScope();

        var container = builder.Build();
    }
}
