using System;

using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities;

namespace DevBridge.Templates.WebProject.Data.EventListeners
{
    public class EventListenerHelper
    {
        private readonly IPrincipalAccessor principalAccessor;

        private string PrincipalName
        {
            get
            {
                var principal = principalAccessor.GetCurrentPrincipal();
                string name = null;

                if (principal != null)
                {
                    name = principal.Identity.Name;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Anonymous";
                }

                return name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventListenerHelper" /> class.
        /// </summary>        
        public EventListenerHelper(IPrincipalAccessor principalAccessor)
        {
            this.principalAccessor = principalAccessor;
        }

        /// <summary>
        /// Called when modifying entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void OnModify(object entity)
        {
            var savingEntity = entity as IEntity;
            if (savingEntity != null)
            {
                savingEntity.ModifiedOn = DateTime.Now;
                savingEntity.ModifiedBy = PrincipalName;
            }
        }

        /// <summary>
        /// Called when creating entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void OnCreate(object entity)
        {
            var savingEntity = entity as IEntity;
            if (savingEntity != null)
            {
                savingEntity.CreatedOn = DateTime.Now;
                savingEntity.CreatedBy = PrincipalName;
                savingEntity.ModifiedOn = DateTime.Now;
                savingEntity.ModifiedBy = PrincipalName;
            }
        }

        /// <summary>
        /// Called when deleting entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void OnDelete(object entity)
        {
            var deletingEntity = entity as IEntity;
            if (deletingEntity != null)
            {                
                deletingEntity.DeletedOn = DateTime.Now;
                deletingEntity.DeletedBy = PrincipalName;
            }
        }
    }
}
