using Ninject;
using Ninject.Modules;
using Sigs.AutorizacionesOnline.Models;
using Sigs.AutorizacionesOnline.Models.Moqs.Services;
using Sigs.AutorizacionesOnline.Models.Services;
//using Semma.ActivosFijos.Models.FakeActivosFijos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Semma.ActivosFijos.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        // A Ninject "kernel" is the thing that can supply object instances
        private IKernel kernel = new StandardKernel(new AutorizacionesInjectionServices());
        // ASP.NET MVC calls this to get the controller for each request
        protected override IController GetControllerInstance(RequestContext context,
        Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }
        // Configures how abstract service types are mapped to concrete implementations
        private class AutorizacionesInjectionServices : NinjectModule
        {
            public override void Load()
            {
                //Bind<IActivosFijos>()
                //    .To<ActivoFijoRepository>();

                //Bind<Validator>()
                //     .To<Validator>();


                //Bind<IActivosFijos>()
                //.To<FakeActivosFijosRepository>()
                //.WithConstructorArgument("connectionString",
                //ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString);

                //------------

                Bind<IAfiliadoService>()
                    .To<AfiliadoService>();

                Bind<IAutorizacionService>()
                    .To<AutorizacionService>().WithConstructorArgument("db", new ArsDataContext());
                //.WithConstructorArgument("db", new SemmaContext());

                /*
                Bind<IActivosFijos>()
                .To<FakeActivosFijosRepository>()
                .WithConstructorArgument("connectionString",
                ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString);
                */
            }
        }
    }
}