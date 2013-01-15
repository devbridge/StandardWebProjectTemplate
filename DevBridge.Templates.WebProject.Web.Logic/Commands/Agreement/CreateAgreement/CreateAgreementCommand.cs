using System;

using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Tools.Commands;
using DevBridge.Templates.WebProject.Tools.Messages;
using DevBridge.Templates.WebProject.Web.Logic.Models.Agreement;

namespace DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.CreateAgreement
{
    public class CreateAgreementCommand : CommandBase, ICommand<CreateAgreementViewModel, bool>
    {
        private IRepository repository;

        private IAgreementManagementService agreementManagementService;

        public CreateAgreementCommand(IRepository repository, IAgreementManagementService agreementManagementService)
        {
            this.agreementManagementService = agreementManagementService;
            this.repository = repository;
        }

        bool ICommand<CreateAgreementViewModel, bool>.Execute(CreateAgreementViewModel request)
        {
            DataEntities.Entities.Agreement agreement = new DataEntities.Entities.Agreement();
            agreement.Customer = repository.AsProxy<Customer>(request.CustomerId);
            agreement.Number = agreementManagementService.GenerateAgreementNumber();
            
            repository.Save(agreement);
            repository.Commit();

            return true;
        }
    }
}