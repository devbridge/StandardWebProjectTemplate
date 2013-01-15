using FluentNHibernate.Mapping;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    /// <summary>
    /// Fluent nHibernate entity map base class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to map.</typeparam>
    public abstract class EntityMapBase<TEntity> : ClassMap<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMapBase{TEntity}" /> class.
        /// </summary>
        protected EntityMapBase()
        {
            Map(x => x.CreatedOn).Not.Nullable().LazyLoad();
            Map(x => x.ModifiedOn).Not.Nullable().LazyLoad();
            Map(x => x.DeletedOn).Nullable().LazyLoad();

            Map(x => x.CreatedBy).Not.Nullable().Length(50).LazyLoad();
            Map(x => x.ModifiedBy).Not.Nullable().Length(50).LazyLoad();
            Map(x => x.DeletedBy).Nullable().Length(50).LazyLoad();
        }
    }
}
