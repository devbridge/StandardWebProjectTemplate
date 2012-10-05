using System;
using System.Collections.Generic;
using System.Linq;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.ServiceContracts;

namespace DevBridge.Templates.WebProject.Services
{
    public class AgreementManagementService : IAgreementManagementService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IAgreementRepository agreementRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly AgreementManagementServiceConfiguration configuration;

        public AgreementManagementService(IAgreementRepository agreementRepository,
                                          ICustomerRepository customerRepository,
                                          IUnitOfWorkFactory unitOfWorkFactory,
                                          IConfigurationLoaderService configurationLoaderService)
        {
            configuration = configurationLoaderService.LoadConfig<AgreementManagementServiceConfiguration>();
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.agreementRepository = agreementRepository;
            this.customerRepository = customerRepository;
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
                using (IUnitOfWork unitOfWork = unitOfWorkFactory.New())
                {
                    try
                    {
                        unitOfWork.BeginTransaction();
                        customerRepository.Use(unitOfWork);
                        agreementRepository.Use(unitOfWork);

                        Customer customer = customerRepository.First(customerId);
                        foreach (var agreement in customer.Agreements)
                        {
                            agreementRepository.Delete(agreement);
                        }

                        Agreement extendedAgreement = new Agreement();
                        extendedAgreement.Customer = customer;
                        extendedAgreement.Number = GenerateAgreementNumber();
                        extendedAgreement.CreatedOn = DateTime.Now;
                        agreementRepository.Save(extendedAgreement);
                        unitOfWork.Commit();

                        return extendedAgreement;
                    }
                    catch (Exception)
                    {
                        unitOfWork.Rollback();
                        throw;
                    }
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
                return agreementRepository.AsQueryable().ToList();
            }
            catch (Exception ex)
            {                
                throw new AgreementManagementException("Failed to retrieve agreements list.", ex);
            }            
        }
    }
}
