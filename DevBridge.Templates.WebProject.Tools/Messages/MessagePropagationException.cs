using System;

namespace DevBridge.Templates.WebProject.Tools.Messages
{
    /// <summary>
    /// Validation exception with attached resource function.
    /// </summary>
    [Serializable]
    public class MessagePropagationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePropagationException" /> class.
        /// </summary>
        /// <param name="messageResource">A function to retrieve a globalized resource associated with this exception.</param>
        /// <param name="internalMessage">The exception message.</param>
        public MessagePropagationException(Func<Exception, string> messageResource, string internalMessage)
            : base(internalMessage)
        {
            Resource = messageResource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePropagationException" /> class.
        /// </summary>
        /// <param name="messageResource">A function to retrieve a globalized resource associated with this exception.</param>
        /// <param name="internalMessage">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MessagePropagationException(Func<Exception, string> messageResource, string internalMessage, Exception innerException)
            : base(internalMessage, innerException)
        {
            Resource = messageResource;
        }

        /// <summary>
        /// Gets a function to retrieve a globalized resource associated with this exception.
        /// </summary>
        /// <value>
        /// A function to retrieve a globalized resource associated with this exception.
        /// </value>
        public Func<Exception, string> Resource { get; private set; }
    }
}
