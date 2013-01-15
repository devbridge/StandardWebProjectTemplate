using DevBridge.Templates.WebProject.DataContracts;

using NHibernate;

namespace DevBridge.Templates.WebProject.Data
{
    public class Repository: GenericRepositoryBase<int>, IRepository
    {
        public Repository(ISessionFactoryProvider sessionFactoryProvider)
            : base(sessionFactoryProvider)
        {
        }

        public Repository(ISession session)
            : base(session)
        {
        }
    }  
}
