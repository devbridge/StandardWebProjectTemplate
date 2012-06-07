using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataContracts.Exceptions;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Services;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace DevBridge.Templates.WebProject.Tests.Services
{
    [TestFixture]
    public class RegistrationServiceTest
    {
        [Test]
        public void Should_Register_Customer_Successfully()
        {            
            using (IUnitOfWork unitOfWork = Singleton.UnitOfWorkFactory.New())
            {
                // TODO: add test here.
            }
        }

        [Test]
        public void Should_Unregister_Customer_Successfully()
        {
            using (IUnitOfWork unitOfWork = Singleton.UnitOfWorkFactory.New())
            {
                // TODO: add test here.
            }
        }

        [Test]
        public void Should_Fail_If_Try_Unregister_Not_Existing_Customer()
        {
            IRegistrationService registrationService = Singleton.UnityContainer.Resolve<IRegistrationService>();
            RegistrationException registrationException = Assert.Catch<RegistrationException>(delegate
                                                                                                  {
                                                                                                      registrationService.UnregisterCustomer(-1);
                                                                                                  });
            Assert.IsTrue(registrationException.InnerException is EntityNotFoundException);
        }
    }
}
