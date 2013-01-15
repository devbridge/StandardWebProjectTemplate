using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.DataEntityMappings
{
    [TestFixture]
    public class AgreementMapTest : DatabaseTestBase<int>
    {
        [Test]
        public void Should_Check_Agreement_Mappings_Successfully()
        {
            var entity = TestDataProvider.CreateCustomer();
            RunEntityMapTestsInTransaction(entity);            
        }   
    }
}
