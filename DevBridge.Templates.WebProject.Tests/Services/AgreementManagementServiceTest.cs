using System;
using System.Linq;
using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataContracts.Exceptions;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Services;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using Moq;
using NHibernate.Linq;
using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.Services
{
    [TestFixture]
    public class AgreementManagementServiceTest
    {
        [Test]
        public void Should_Not_Extend_Agreement_For_Not_Existing_Customer()
        {
            using (IUnitOfWork unitOfWork = Singleton.UnitOfWorkFactory.New())
            {
                ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                IAgreementRepository agreementRepository = new AgreementRepository(unitOfWork);

                IAgreementManagementService agreementManagementService =
                    new AgreementManagementService(agreementRepository,
                                                   customerRepository,
                                                   Singleton.UnitOfWorkFactory, Singleton.ConfigurationLoaderService);

                Exception ex = Assert.Catch<AgreementManagementException>(delegate
                                                                              {
                                                                                  agreementManagementService.
                                                                                      ExtendAgreement(-1);
                                                                              });
                Assert.IsTrue(ex.InnerException is EntityNotFoundException);
            }
        }

        [Test]
        public void Should_Extend_Agreement_Successfully()
        {
            using (IUnitOfWork unitOfWork = Singleton.UnitOfWorkFactory.New())
            {
                unitOfWork.BeginTransaction();

                var newCustomer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                unitOfWork.Session.SaveOrUpdate(newCustomer);

                var newAgreement = Singleton.TestDataProvider.CreateNewRandomAgreementForCustomer(newCustomer);
                unitOfWork.Session.SaveOrUpdate(newAgreement);

                ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                IAgreementRepository agreementRepository = new AgreementRepository(unitOfWork);

                Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
                unitOfWorkMock.Setup(f => f.BeginTransaction()).Verifiable();
                unitOfWorkMock.Setup(f => f.Commit()).Callback(() => unitOfWork.Session.Flush()).Verifiable();
                unitOfWorkMock.Setup(f => f.Dispose()).Verifiable();
                unitOfWorkMock.Setup(f => f.Session).Returns(unitOfWork.Session);

                Mock<IUnitOfWorkFactory> unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactoryMock.Setup(f => f.New()).Returns(unitOfWorkMock.Object);

                IAgreementManagementService agreementManagementService =
                    new AgreementManagementService(agreementRepository,
                                                   customerRepository,
                                                   unitOfWorkFactoryMock.Object,
                                                   Singleton.ConfigurationLoaderService);

                var customer = agreementRepository.AsQueryable().Fetch(f => f.Customer).First().Customer;
                Assert.IsNotNull(customer);
                
                var agreement = agreementManagementService.ExtendAgreement(customer.Id);
                Assert.IsNotNull(agreement);
                Assert.AreEqual(agreement.Customer, customer);

                unitOfWorkMock.Verify();
            }
        }

        [Test]
        public void Should_Generate_Valid_Agreement_Number()
        {
            IAgreementManagementService service = new AgreementManagementService(null, null, null, Singleton.ConfigurationLoaderService);
            var config = Singleton.ConfigurationLoaderService.LoadConfig<AgreementManagementServiceConfiguration>();
            string number = service.GenerateAgreementNumber();
            Assert.IsNotNull(number);
            Assert.AreEqual(20, number.Length);
            Assert.IsTrue(number.StartsWith(config.AgreementCodePrefix));
        }

        [Test]
        public void Should_Retrieve_Agreement_List()
        {
            using (IUnitOfWork unitOfWork = Singleton.UnitOfWorkFactory.New())
            {
                unitOfWork.BeginTransaction();

                var customer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                unitOfWork.Session.SaveOrUpdate(customer);

                var agreement = Singleton.TestDataProvider.CreateNewRandomAgreementForCustomer(customer);
                unitOfWork.Session.SaveOrUpdate(agreement);

                IAgreementManagementService agreementManagementService =
                    new AgreementManagementService(new AgreementRepository(unitOfWork), 
                                                   new CustomerRepository(unitOfWork), 
                                                   Singleton.UnitOfWorkFactory,
                                                   Singleton.ConfigurationLoaderService);

                var list = agreementManagementService.GetAgreements();
                Assert.IsNotNull(list);
                Assert.Greater(list.Count, 0);
            }
        }
    }
}
