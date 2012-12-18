using DevBridge.Templates.WebProject.Tools.Commands;

namespace BetterCms.Core.Mvc.Commands
{
    public interface ICommandResolver
    {
        TCommand ResolveCommand<TCommand>(ICommandContext context) where TCommand : ICommandBase;
    }
}
