using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.ServiceContracts;

using NHibernate.Linq;

namespace DevBridge.Templates.WebProject.Services
{
    public class AgreementManagementService : IAgreementManagementService
    {
        private readonly AgreementManagementServiceConfiguration configuration;

        private IRepository repository;

        public AgreementManagementService(IRepository repository,
                                          IConfigurationLoaderService configurationLoaderService)
        {
            this.repository = repository;
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
                using (var transaction = new TransactionScope())
                {
                    Customer customer = repository.First<Customer>(customerId);

                    if (customer != null)
                    {
                        foreach (var agreement in customer.Agreements)
                        {
                            repository.Delete(agreement);
                        }
                    }

                    Agreement extendedAgreement = new Agreement();
                    extendedAgreement.Customer = customer;
                    extendedAgreement.Number = GenerateAgreementNumber();
                    extendedAgreement.CreatedOn = DateTime.Now;

                    repository.Save(extendedAgreement);
                    repository.Commit();

                    transaction.Complete();

                    return extendedAgreement;
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
                var query = repository.AsQueryOver<Agreement>().Where(f => f.DeletedOn == null).Future();
                
                return query.ToList();
            }
            catch (Exception ex)
            {                
                throw new AgreementManagementException("Failed to retrieve agreements list.", ex);
            }
        }
    }
}
