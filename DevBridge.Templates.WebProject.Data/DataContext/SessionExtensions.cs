using System;
using System.Linq;

using NHibernate;
using NHibernate.Engine;
using NHibernate.Persister.Entity;
using NHibernate.Proxy;

namespace DevBridge.Templates.WebProject.Data.DataContext
{
    public static class SessionExtensions
    {
        public static Boolean IsDirtyEntity(this ISession session, Object entity)
        {
            ISessionImplementor sessionImpl = session.GetSessionImplementation();
            IPersistenceContext persistenceContext = sessionImpl.PersistenceContext;
            EntityEntry oldEntry = persistenceContext.GetEntry(entity);
            string className = oldEntry.EntityName;
            IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);

            if (oldEntry == null && entity is INHibernateProxy)
            {
                INHibernateProxy proxy = entity as INHibernateProxy;
                Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);
                oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);
            }

            object[] oldState = oldEntry.LoadedState;
            if (oldState == null)
            {
                return false;
            }

            object[] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);
            int[] dirtyProps = oldState.Select((o, i) => (oldState[i] == currentState[i]) ? -1 : i).Where(x => x >= 0).ToArray();

            return (dirtyProps != null && dirtyProps.Length > 0);
        }       
    }
}
