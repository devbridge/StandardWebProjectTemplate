using System.Web.Mvc;

using BetterCms.Core.Mvc.Commands;

using Common.Logging;

using DevBridge.Templates.WebProject.Tools.Commands;
using DevBridge.Templates.WebProject.Tools.Messages;
using Microsoft.Practices.Unity;

namespace DevBridge.Templates.WebProject.Tools.Mvc
{
    public abstract class ExtendedControllerBase : Controller, ICommandContext
    {
        /// <summary>
        /// Current class logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// View data key to store messages.
        /// </summary>
        private const string UserMessagesViewDataKey = "_UserMessages";

        [Dependency]
        public virtual ICommandResolver CommandResolver { get; set; }

        /// <summary>
        /// Gets the user messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public virtual IMessagesIndicator Messages
        {
            get
            {
                var userMessages = ViewData[UserMessagesViewDataKey] as UserMessages;

                if (userMessages == null)
                {
                    userMessages = new UserMessages();
                    ViewData[UserMessagesViewDataKey] = userMessages;
                }

                return userMessages;
            }
        }

        protected TCommand GetCommand<TCommand>() where TCommand : ICommandBase
        {
            return CommandResolver.ResolveCommand<TCommand>(this);
        }
    }
}
