using System;

namespace DevBridge.Templates.WebProject.DataEntities
{
    public abstract class PersistentEntityBase<TEntity> : EntityBase<TEntity>, IPersistentEntity<int>
        where TEntity : class, IPersistentEntity<int>
    {
        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

        public virtual DateTime ModifiedOn { get; set; }

        public virtual string ModifiedBy { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual string DeletedBy { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, CreatedOn: {1}, DeletedOn: {2}", base.ToString(), CreatedOn, DeletedOn);
        }
    }
}