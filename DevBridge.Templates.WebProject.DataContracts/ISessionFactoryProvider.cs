using System;
using NHibernate;

namespace DevBridge.Templates.WebProject.DataContracts
{
    public interface ISessionFactoryProvider
    {
        ISessionFactory SessionFactory { get; }
    }
}
