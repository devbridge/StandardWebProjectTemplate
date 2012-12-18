using System;

using DevBridge.Templates.WebProject.DataEntities;

namespace DevBridge.Templates.WebProject.DataContracts.Exceptions
{
    /// <summary>
    /// Concurrent data exception
    /// </summary>
    public class ConcurrentDataException : DataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentDataException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ConcurrentDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentDataException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ConcurrentDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentDataException" /> class.
        /// </summary>
        /// <param name="staleEntity">The stale entity.</param>
        public ConcurrentDataException(IEntity staleEntity)
            : base(string.Format("{0} with id {1} Entity was updated by another transaction.", staleEntity.GetType().Name, staleEntity.Id))
        {
            StaleEntity = staleEntity;
        }

        /// <summary>
        /// Gets or sets the stale entity.
        /// </summary>
        public IEntity StaleEntity { get; set; }
    }
}
