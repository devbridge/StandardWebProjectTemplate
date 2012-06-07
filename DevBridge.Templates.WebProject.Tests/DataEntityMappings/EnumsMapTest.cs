using System;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Enums;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.DataEntityMappings
{
    [TestFixture]
    public class EnumsMapTest
    {
        [Test]
        public void Should_Match_All_CustomerType_Values()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                var customerTypeQuery = unitOfWork.Session.CreateSQLQuery("select Type from CustomerType");
                var dbCustomerTypes = customerTypeQuery.List<int>();
                
                Assert.AreEqual(dbCustomerTypes.Count, Enum.GetNames(typeof(CustomerType)).Length);
                foreach (var dbCustomerType in dbCustomerTypes)
                {
                    Assert.IsTrue(Enum.IsDefined(typeof(CustomerType), dbCustomerType), string.Format("CustomerType enum {0} conflict with database CustomerType table.", dbCustomerType));
                }
            }
        }
        
    }
}
