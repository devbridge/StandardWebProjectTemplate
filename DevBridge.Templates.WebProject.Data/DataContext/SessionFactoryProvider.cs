using DevBridge.Templates.WebProject.Data.DataContext.Conventions;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace DevBridge.Templates.WebProject.Data.DataContext
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private readonly static object lockObject = new object();

        private volatile ISessionFactory sessionFactory;

        public ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (lockObject)
                    {
                        if (sessionFactory == null)
                        {
                            sessionFactory = CreateSessionFactory();
                        }
                    }
                }

                return sessionFactory;
            }
        }

        private ISessionFactory CreateSessionFactory()
        {            
            return Fluently.Configure()
                 .Mappings(m => m.FluentMappings
                                    .AddFromAssemblyOf<IEntity>()
                                    .Conventions.Add(ForeignKey.EndsWith("Id"))
                                    .Conventions.Add<EnumConvention>())

                 .ExposeConfiguration(c => c.SetInterceptor(new StaleInterceptor()))

                 .BuildConfiguration()
                 .BuildSessionFactory();
        }
    }
}