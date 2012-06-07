using DevBridge.Templates.WebProject.DataContracts;

namespace DevBridge.Templates.WebProject.Data.DataContext
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ISessionFactoryProvider sessionFactoryProvider;

        public UnitOfWorkFactory(ISessionFactoryProvider sessionFactoryProvider)
        {
            this.sessionFactoryProvider = sessionFactoryProvider;
        }

        public IUnitOfWork New()
        {
            return new UnitOfWork(sessionFactoryProvider);
        }
    }
}
