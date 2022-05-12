using MemberAndOrganizationDataCorrectionInEBS.Implementation;
using MemberAndOrganizationDataCorrectionInEBS.Interface;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Utility
{
    public class Instantiator
    {
        private static StandardKernel kernel;

        private static Instantiator singleton;

        public StandardKernel Kernel => kernel;

        public static void Initialize()
        {
            if (singleton == null)
            {
                singleton = new Instantiator();
                kernel = new StandardKernel();
                RegisterServices();
            }
        }

        public static Instantiator GetInstantiator()
        {
            if (singleton == null)
            {
                Initialize();
            }

            return singleton;
        }

        public T Get<T>()
        {
            return kernel.Get<T>();
        }

        public T Get<T>(string bindingName)
        {
            return kernel.Get<T>(bindingName);
        }

        public T Get<T>(params Parameter[] parms)
        {
            return kernel.Get<T>(parms);
        }

        private static void RegisterServices()
        {
            kernel.Bind<ILogger>().To<NLogLogger>();
            kernel.Bind<IMemberSystemLoggerService>().To<MemberSystemLoggerService>();
            kernel.Bind<IExternalService>().To<ExternalService>();
            kernel.Bind<IHttpClientWrapper>().To<HttpClientWrapper>();
        }
    }
}
