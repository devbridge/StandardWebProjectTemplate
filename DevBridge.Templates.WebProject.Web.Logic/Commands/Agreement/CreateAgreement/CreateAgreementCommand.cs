using System;

using DevBridge.Templates.WebProject.Tools.Commands;
using DevBridge.Templates.WebProject.Web.Logic.Models.Agreement;

namespace DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.CreateAgreement
{
    public class CreateAgreementCommand : CommandBase, ICommand<CreateAgreementViewModel, bool>
    {
        public bool Execute(CreateAgreementViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}