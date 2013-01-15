using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult" /> object that serializes the specified object to JavaScript Object Notation (JSON) format.
        /// </summary>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="behavior">Specifies whether HTTP GET requests from the client are allowed.</param>
        /// <returns>
        /// The JSON result object that serializes the specified object to JSON format.
        /// </returns>
        [NonAction]
        public virtual JsonResult WireJson(WireJson data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {            
            List<string> messages = data.Messages != null
                                        ? data.Messages.ToList()
                                        : new List<string>();

            messages.AddRange(data.Success ? Messages.Success : Messages.Error);
            data.Messages = messages.ToArray();

            return base.Json(data, behavior);
        }

        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult" /> object that serializes the specified object to JavaScript Object Notation (JSON) format.
        /// </summary>
        /// <param name="success">The request result.</param>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="behavior">Specifies whether HTTP GET requests from the client are allowed.</param>
        /// <returns>
        /// The JSON result object that serializes the specified object to JSON format.
        /// </returns>
        [NonAction]
        public virtual JsonResult WireJson(bool success, dynamic data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return WireJson(new WireJson { Success = success, Data = data }, behavior);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!ModelState.IsValid)
            {
                var modelStateErrors = from item in ModelState.Values
                                       from error in item.Errors
                                       select error.ErrorMessage;

                Messages.AddError(modelStateErrors.ToArray());
            }

            base.OnActionExecuting(filterContext);
        }           

        protected TCommand GetCommand<TCommand>() where TCommand : ICommandBase
        {
            return CommandResolver.ResolveCommand<TCommand>(this);
        }
    }
}
