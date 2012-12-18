using DevBridge.Templates.WebProject.Tools.Messages;

namespace DevBridge.Templates.WebProject.Tools.Commands
{
    public interface ICommandContext
    {
        IMessagesIndicator Messages { get; }
    }    
}
