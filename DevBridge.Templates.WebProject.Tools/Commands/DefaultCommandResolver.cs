using System.Web.Mvc;

using BetterCms.Core.Mvc.Commands;

namespace DevBridge.Templates.WebProject.Tools.Commands
{
    public class DefaultCommandResolver : ICommandResolver
    {
        public TCommand ResolveCommand<TCommand>(ICommandContext context) where TCommand : ICommandBase
        {
            var command = DependencyResolver.Current.GetService<TCommand>();
            command.Context = context;

            return command;
        }
    }
}
