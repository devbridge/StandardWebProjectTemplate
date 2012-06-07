using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Services;
using Microsoft.Practices.Unity;

namespace DevBridge.Templates.WebProject.Tests.TestHelpers
{
    public static class Singleton
    {
        public static readonly IUnityContainer UnityContainer;        

        public readonly static ISessionFactoryProvider SessionFactoryProvider;

        public static readonly IUnitOfWorkFactory UnitOfWorkFactory;

        public static readonly IConfigurationLoaderService ConfigurationLoaderService;

        public static readonly RandomTestDataProvider TestDataProvider;        

        static Singleton()
        {
            UnityContainer = CreateUnityContainer();
            SessionFactoryProvider = UnityContainer.Resolve<ISessionFactoryProvider>();
            UnitOfWorkFactory = UnityContainer.Resolve<IUnitOfWorkFactory>();
            ConfigurationLoaderService = UnityContainer.Resolve<IConfigurationLoaderService>();

            TestDataProvider = new RandomTestDataProvider();
        }

        private static IUnityContainer CreateUnityContainer()
        {
            return new UnityContainer()
                .RegisterType<ISessionFactoryProvider, SessionFactoryProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
                
                .RegisterType<IAgreementManagementService, AgreementManagementService>(new HierarchicalLifetimeManager())
                .RegisterType<IRegistrationService, RegistrationService>(new HierarchicalLifetimeManager())
                .RegisterType<IDataListingService, DataListingService>(new HierarchicalLifetimeManager())

                .RegisterType<IAgreementRepository, AgreementRepository>(new HierarchicalLifetimeManager())
                .RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager())
                
                .RegisterType<IConfigurationLoaderService, ConfigurationLoaderService>(new ContainerControlledLifetimeManager());
        }
    }
}
