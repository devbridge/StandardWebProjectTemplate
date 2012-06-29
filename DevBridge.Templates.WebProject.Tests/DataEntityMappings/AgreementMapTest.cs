using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.DataEntityMappings
{
    [TestFixture]
    public class AgreementMapTest
    {
        [Test]
        public void Should_Check_Agreement_Mappings_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                unitOfWork.BeginTransaction();

                Customer customer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                unitOfWork.Session.SaveOrUpdate(customer);

                new PersistenceSpecification<Agreement>(unitOfWork.Session)
                    .CheckProperty(f => f.Number, Singleton.TestDataProvider.ProvideRandomString(20))
                    .CheckProperty(f => f.DeletedOn, null)
                    .CheckProperty(f => f.CreatedOn, Singleton.TestDataProvider.ProvideRandomDateTime())
                    .CheckReference(f => f.Customer, customer)
                    .VerifyTheMappings();
            }
        }   
    }
}
