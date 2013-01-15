using System;

using Autofac;

using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataContracts.Exceptions;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Services;

using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.Services
{
    [TestFixture]
    public class AgreementManagementServiceTest : DatabaseTestBase<int>
    {
        [Test]
        public void Should_Not_Extend_Agreement_For_Not_Existing_Customer()
        {
            IAgreementManagementService agreementManagementService = new AgreementManagementService(Container.Resolve<IRepository>(), Container.Resolve<IConfigurationLoaderService>());

            Exception ex = Assert.Catch<AgreementManagementException>(
                delegate
                    {
                        agreementManagementService.ExtendAgreement(-1);
                    });

            Assert.IsTrue(ex.InnerException is EntityNotFoundException);
        }

        //[Test]
        //public void Should_Extend_Agreement_Successfully()
        //{
        //    RunActionInTransaction(session =>
        //        {
        //            var newCustomer = TestDataProvider.CreateCustomer();
        //            session.SaveOrUpdate(newCustomer);

        //            var newAgreement = TestDataProvider.CreateAgreement(newCustomer);
        //            session.SaveOrUpdate(newAgreement);

        //            IAgreementManagementService agreementManagementService = new AgreementManagementService(new Repository(session), Singleton.ConfigurationLoaderService);

        //            var customer = unitOfWork.Session.Query<Agreement>().Fetch(f => f.Customer).First().Customer;
        //            Assert.IsNotNull(customer);

        //            var agreement = agreementManagementService.ExtendAgreement(customer.Id);
        //            Assert.IsNotNull(agreement);
        //            Assert.AreEqual(agreement.Customer, customer);

        //            unitOfWorkMock.Verify();
        //        });
        //}

        //[Test]
        //public void Should_Generate_Valid_Agreement_Number()
        //{
        //    IAgreementManagementService service = new AgreementManagementService(null, Singleton.ConfigurationLoaderService);
        //    var config = Singleton.ConfigurationLoaderService.LoadConfig<AgreementManagementServiceConfiguration>();
        //    string number = service.GenerateAgreementNumber();
        //    Assert.IsNotNull(number);
        //    Assert.AreEqual(20, number.Length);
        //    Assert.IsTrue(number.StartsWith(config.AgreementCodePrefix));
        //}

        //[Test]
        //public void Should_Retrieve_Agreement_List()
        //{
        //    using (IUnitOfWork unitOfWork = Singleton.UnitOfWorkFactory.New())
        //    {
        //        unitOfWork.BeginTransaction();

        //        var customer = Singleton.TestDataProvider.CreateNewRandomCustomer();
        //        unitOfWork.Session.SaveOrUpdate(customer);

        //        var agreement = Singleton.TestDataProvider.CreateNewRandomAgreementForCustomer(customer);
        //        unitOfWork.Session.SaveOrUpdate(agreement);

        //        IAgreementManagementService agreementManagementService =
        //            new AgreementManagementService(unitOfWork, Singleton.ConfigurationLoaderService);

        //        var list = agreementManagementService.GetAgreements();
        //        Assert.IsNotNull(list);
        //        Assert.Greater(list.Count, 0);
        //    }
        //}
    }
}
