using System;
using System.Linq;
using System.Transactions;

using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.DataEntities.Enums;
using DevBridge.Templates.WebProject.Tests.TestHelpers;

using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.DataContracts
{
    [TestFixture]
    public class CustomerRepositoryTest
    {
        [Test]
        public void Should_Use_Two_Transactions_In_One_TransactionScope()
        {
            string test_name = Guid.NewGuid().ToString();

            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                var customer = customerRepository.AsQueryable(f => f.Name == test_name).FirstOrDefault();
                Assert.IsNull(customer);
            }

            using (new TransactionScope(TransactionScopeOption.Required))
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    unitOfWork.BeginTransaction();

                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    customerRepository.Save(
                        new Customer
                        {
                            Code = "test_code",
                            Name = test_name,
                            Type = CustomerType.LoyalCustomers,
                            CreatedOn = DateTime.Now,
                            DeletedOn = null
                        });

                    unitOfWork.Commit();
                }

                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    unitOfWork.BeginTransaction();

                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    var customer = customerRepository.AsQueryable(f => f.Name == test_name).FirstOrDefault();
                    Assert.IsNotNull(customer);

                    unitOfWork.Commit();
                }
            }
        }
    }
}
