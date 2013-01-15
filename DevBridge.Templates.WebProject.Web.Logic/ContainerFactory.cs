using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Autofac;
using Autofac.Integration.Mvc;

using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.Services;
using DevBridge.Templates.WebProject.Tools.Commands;
using DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.GetAgreements;

namespace DevBridge.Templates.WebProject.Web.Logic
{
    public class ContainerFactory
    {        
        public IContainer CreateContainer(params Assembly[] webAssemblies)
        {
            var builder = new ContainerBuilder();
            
            if (webAssemblies != null)
            {
                foreach (var webAssembly in webAssemblies)
                {
                    builder.RegisterControllers(webAssembly).InstancePerHttpRequest().PropertiesAutowired();
                }
            }

            builder.RegisterType<SessionFactoryProvider>().As<ISessionFactoryProvider>().SingleInstance();
            builder.RegisterType<WebPrincipalAccessor>().As<IPrincipalAccessor>().SingleInstance();
            builder.RegisterType<CachingService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConfigurationLoaderService>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<Repository>().As<IRepository>().InstancePerHttpRequest();
            builder.RegisterType<AgreementManagementService>().AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterType<RegistrationService>().AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterType<DataListingService>().AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterType<DefaultCommandResolver>().AsImplementedInterfaces().InstancePerHttpRequest();

            RegisterCommands(typeof(GetAgreementsCommand).Assembly, builder);

            return builder.Build();
        }

        /// <summary>
        /// Registers the module command types.
        /// </summary>
        private void RegisterCommands(Assembly assembly, ContainerBuilder containerBuilder)
        {
            Type[] commandTypes = new[]
                {
                    typeof(ICommand),
                    typeof(ICommand<>),
                    typeof(ICommand<,>)
                };

            containerBuilder
                .RegisterAssemblyTypes(assembly)
                .Where(scan => commandTypes.Any(commandType => IsAssignableToGenericType(scan, commandType)))
                .AsImplementedInterfaces()
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerHttpRequest();
        }

        /// <summary>
        /// Determines whether given type is assignable to generic type.
        /// </summary>
        /// <param name="givenType">A given type.</param>
        /// <param name="genericType">A generic type.</param>
        /// <returns>
        ///   <c>true</c> if given type is assignable to generic type; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            Type baseType = givenType.BaseType;
            if (baseType == null)
            {
                return false;
            }

            return (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == genericType) || IsAssignableToGenericType(baseType, genericType);
        }
    }
}
