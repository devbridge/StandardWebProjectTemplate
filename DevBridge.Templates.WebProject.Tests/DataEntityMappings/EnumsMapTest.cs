using System;

using DevBridge.Templates.WebProject.DataEntities.Enums;

using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.DataEntityMappings
{
    [TestFixture]
    public class EnumsMapTest : DatabaseTestBase<int>
    {
        [Test]
        public void Should_Match_All_CustomerType_Values()
        {
            RunActionInTransaction(session =>
                {
                    var customerTypeQuery = session.CreateSQLQuery("select Type from CustomerType");
                    var dbCustomerTypes = customerTypeQuery.List<int>();

                    Assert.AreEqual(dbCustomerTypes.Count, Enum.GetNames(typeof(CustomerType)).Length);
                    foreach (var dbCustomerType in dbCustomerTypes)
                    {
                        Assert.IsTrue(Enum.IsDefined(typeof(CustomerType), dbCustomerType), string.Format("CustomerType enum {0} conflict with database CustomerType table.", dbCustomerType));
                    }        
                });
        }
    }
}
