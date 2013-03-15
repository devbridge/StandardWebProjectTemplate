using FluentNHibernate.Mapping;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public abstract class EntityMapBase<TEntity> : ClassMap<TEntity>
        where TEntity : IEntity
    {
    }
}
