using DevBridge.Templates.WebProject.DataContracts;

using Microsoft.Practices.Unity;

namespace DevBridge.Templates.WebProject.Tools.Commands
{
    public abstract class CommandBase : ICommandBase
    {
        /// <summary>
        /// Gets or sets a command context.
        /// </summary>
        /// <value>
        /// A command executing context.
        /// </value>
        public ICommandContext Context { get; set; }

        /// <summary>
        /// Gets or sets the unit of work. This property is auto wired.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        [Dependency]
        public virtual IUnitOfWork UnitOfWork { get; set; }
    }
}
