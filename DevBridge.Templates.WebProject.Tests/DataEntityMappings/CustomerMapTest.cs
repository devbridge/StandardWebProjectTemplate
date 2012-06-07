using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.DataEntities.Enums;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.DataEntityMappings
{
    [TestFixture]
    public class CustomerMapTest
    {
        [Test]
        public void Should_Check_Customer_Mappings_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                unitOfWork.BeginTransaction();

                new PersistenceSpecification<Customer>(unitOfWork.Session)
                    .CheckProperty(f => f.Name, Singleton.TestDataProvider.ProvideRandomString(50))
                    .CheckProperty(f => f.Code, Singleton.TestDataProvider.ProvideRandomString(10))
                    .CheckProperty(f => f.Type, CustomerType.NeedBasedCustomers)
                    .CheckProperty(f => f.IsDeleted, false)
                    .CheckProperty(f => f.CreatedOn, Singleton.TestDataProvider.ProvideRandomDateTime())
                    .VerifyTheMappings();
            }
        }
    }
}
