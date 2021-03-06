[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DesafioNoxApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DesafioNoxApi.App_Start.NinjectWebCommon), "Stop")]

namespace DesafioNoxApi.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using DesafioNoxApi.Repositorios.Interfaces;
    using DesafioNoxApi.Repositorios;
    using DesafioNoxApi.Servicos.Interfaces;
    using DesafioNoxApi.Servicos;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
            kernel.Bind<ITransacaoRepositorio>().To<TransacaoRepositorio>().InRequestScope();
            kernel.Bind<IUsuarioRepositorio>().To<UsuarioRepositorio>().InRequestScope();
            kernel.Bind<ITransacaoServico>().To<TransacaoServico>().InRequestScope();
            kernel.Bind<IUsuarioServico>().To<UsuarioServico>().InRequestScope();
            
        }
    }
}