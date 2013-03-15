using System;

using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities;

namespace DevBridge.Templates.WebProject.Data.EventListeners
{
public class EventListenerHelper
    {
        private readonly IPrincipalAccessor principalAccessor;

        public EventListenerHelper(IPrincipalAccessor principalAccessor)
        {
            this.principalAccessor = principalAccessor;
        }

        private string PrincipalName
        {
            get
            {
                var principal = principalAccessor.GetCurrentPrincipal();

                string name = null;

                if (principal != null && principal.Identity != null)
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

        public void OnModify(object entity)
        {
            var savingEntity = entity as IPersistentEntity;

            if (savingEntity != null)
            {
                savingEntity.ModifiedOn = DateTime.Now;

                if (string.IsNullOrEmpty(savingEntity.ModifiedBy))
                {
                    savingEntity.ModifiedBy = PrincipalName;
                }
            }
        }

        public void OnCreate(object entity)
        {
            var savingEntity = entity as IPersistentEntity;

            if (savingEntity != null)
            {
                savingEntity.CreatedOn = DateTime.Now;
                savingEntity.ModifiedOn = DateTime.Now;

                if (string.IsNullOrEmpty(savingEntity.CreatedBy))
                {
                    savingEntity.CreatedBy = PrincipalName;
                }

                if (string.IsNullOrEmpty(savingEntity.ModifiedBy))
                {
                    savingEntity.ModifiedBy = PrincipalName;
                }
            }
        }

        public void OnDelete(object entity)
        {
            var deletingEntity = entity as IPersistentEntity;

            if (deletingEntity != null)
            {
                deletingEntity.DeletedOn = DateTime.Now;

                if (string.IsNullOrEmpty(deletingEntity.DeletedBy))
                {
                    deletingEntity.DeletedBy = PrincipalName;
                }
            }
        }
    }
}
