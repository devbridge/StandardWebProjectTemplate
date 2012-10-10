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
    public class AgreementRepositoryTest
    {
        [Test]
        public void Should_Get_Agreement_By_Customer_Type_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                unitOfWork.BeginTransaction();

                var customer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                customer.Type = CustomerType.ImpulseCustomers;
                unitOfWork.Session.SaveOrUpdate(customer);

                var agreement = Singleton.TestDataProvider.CreateNewRandomAgreementForCustomer(customer);
                unitOfWork.Session.SaveOrUpdate(agreement);
                unitOfWork.Session.Flush();

                IAgreementRepository agreementRepository = new AgreementRepository(unitOfWork);
                var list = agreementRepository.GetAgreementsByCustomerType(CustomerType.ImpulseCustomers);

                Assert.IsNotNull(list);
                Assert.IsTrue(list.Contains(agreement));
            }
        }

        private void CreateCustomer(string name)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                unitOfWork.BeginTransaction();

                ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);

                customerRepository.Save(
                    new Customer
                    {
                        Code = "test_code",
                        Name = name,
                        Type = CustomerType.LoyalCustomers,
                        CreatedOn = DateTime.Now,
                        DeletedOn = null
                    });

                unitOfWork.Commit();
            }
        }
    }
}
