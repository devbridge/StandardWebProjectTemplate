using DevBridge.Templates.WebProject.Tools.Commands;
using DevBridge.Templates.WebProject.Web.Logic.Models.Agreement;

namespace DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.GetAgreements
{
    public class GetAgreementsCommand : CommandBase, ICommand<GetAgreementsFilter, AgreementListViewModel>
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Executed command result.</returns>
        public AgreementListViewModel Execute(GetAgreementsFilter request)
        {
            throw new System.NotImplementedException();
        }

    }
}