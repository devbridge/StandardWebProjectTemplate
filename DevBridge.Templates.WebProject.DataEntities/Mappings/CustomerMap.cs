using DevBridge.Templates.WebProject.DataEntities.Entities;
using FluentNHibernate.Mapping;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public sealed class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Name).Length(50).Not.Nullable();
            Map(f => f.Code).Length(10).Not.Nullable();
            Map(f => f.Type).Not.Nullable();
            HasMany(f => f.Agreements)
                .KeyColumn("CustomerId")
                .Cascade.SaveUpdate();            
            Map(f => f.CreatedOn).Not.Nullable();
            Map(f => f.DeletedOn).Nullable();
        }
    }
}
