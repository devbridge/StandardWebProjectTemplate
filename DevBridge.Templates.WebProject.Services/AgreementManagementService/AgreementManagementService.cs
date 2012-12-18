using System;
using System.Collections.Generic;
using System.Linq;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.ServiceContracts;

using NHibernate.Linq;

namespace DevBridge.Templates.WebProject.Services
{
    public class AgreementManagementService : IAgreementManagementService
    {
        private readonly AgreementManagementServiceConfiguration configuration;

        private IUnitOfWork unitOfWork;

        public AgreementManagementService(IUnitOfWork unitOfWork,
                                          IConfigurationLoaderService configurationLoaderService)
        {
            this.unitOfWork = unitOfWork;
            configuration = configurationLoaderService.LoadConfig<AgreementManagementServiceConfiguration>();
        }

        public string GenerateAgreementNumber()
        {
            try
            {
                DateTime now = DateTime.Now;
                return string.Format("{0}{1:0000000000}{2:0000}{3:00}{4:00}{5:0}", configuration.AgreementCodePrefix, new Random().Next(10000000), now.Year, now.Month, now.Day, now.Minute / 10);
            }
            catch (Exception ex)
            {                
                throw new AgreementManagementException("Failed to generate agreement number.", ex);
            }            
        }

        public Agreement ExtendAgreement(int customerId)
        {
            try
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    Customer customer = unitOfWork.Session.Query<Customer>().FetchMany(f => f.Agreements).FirstOrDefault(f => f.Id == customerId && f.DeletedOn == null);
                    if (customer != null)
                    {
                        foreach (var agreement in customer.Agreements)
                        {
                            unitOfWork.Session.Delete(agreement);
                        }
                    }

                    Agreement extendedAgreement = new Agreement();
                    extendedAgreement.Customer = customer;
                    extendedAgreement.Number = GenerateAgreementNumber();
                    extendedAgreement.CreatedOn = DateTime.Now;

                    unitOfWork.Session.SaveOrUpdate(extendedAgreement);
                    unitOfWork.Commit();

                    return extendedAgreement;
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new AgreementManagementException(string.Format("Failed to extend agreement for customer {0}.", customerId), ex);
            }
        }

        public IList<Agreement> GetAgreements()
        {
            try
            {
                return unitOfWork.Session.Query<Agreement>().Where(f => f.DeletedOn == null).ToList();
            }
            catch (Exception ex)
            {                
                throw new AgreementManagementException("Failed to retrieve agreements list.", ex);
            }
        }
    }
}
