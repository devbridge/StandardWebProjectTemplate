using Autofac;

using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.Services;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using DevBridge.Templates.WebProject.Web.Logic;

namespace DevBridge.Templates.WebProject.Tests
{
    public abstract class TestBase
    {
        private ILifetimeScope container;

        private RandomTestDataProvider testDataProvider;
        
        public ILifetimeScope Container
        {
            get
            {
                if (container == null)
                {
                    container = CreateContainer();
                }

                return container;
            }
        }

        public RandomTestDataProvider TestDataProvider
        {
            get
            {
                if (testDataProvider == null)
                {
                    testDataProvider = new RandomTestDataProvider();
                }
                return testDataProvider;
            }
        }

        private static ILifetimeScope CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<SessionFactoryProvider>().As<ISessionFactoryProvider>().SingleInstance();
            builder.RegisterType<FakePrincipalAccessor>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CachingService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConfigurationLoaderService>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<Repository>().As<IRepository>().InstancePerDependency();
            builder.RegisterType<AgreementManagementService>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<RegistrationService>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<DataListingService>().AsImplementedInterfaces().InstancePerDependency();
                                   
            return builder.Build();
        }      
    }
}
