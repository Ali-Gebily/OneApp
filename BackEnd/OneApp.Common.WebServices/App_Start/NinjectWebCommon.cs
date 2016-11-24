using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Interception.Infrastructure.Language;
using OneApp.Common.WebServices.Interceptors;
using OneApp.Common.Core.Utilities;
using OneApp.Common.Core.Injectors;

[assembly: WebActivator.PreApplicationStartMethod(typeof(OneApp.Common.WebServices.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(OneApp.Common.WebServices.App_Start.NinjectWebCommon), "Stop")]

namespace OneApp.Common.WebServices.App_Start
{

    public static class NinjectWebCommon
    {
        static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);

        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            WebApiConfig.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<System.Web.Http.Dispatcher.IHttpControllerActivator>()
                             .To<System.Web.Http.Dispatcher.DefaultHttpControllerActivator>().Intercept().With<ControllerCreationInterceptor>();
            var oneAppKernel = new OneAppKernel(kernel);
            foreach (var t in OneAppUtility.GetOneAppTypesOfType<IOneAppNinjectResolver>())
            {
                if (!t.IsInterface)
                {
                    var inject = Activator.CreateInstance(t) as IOneAppNinjectResolver;
                    inject.RegisterServices(oneAppKernel);
                }
            }

        }
        public static T Load<T>()
        {
            return bootstrapper.Kernel.Get<T>();

        }
    }
}