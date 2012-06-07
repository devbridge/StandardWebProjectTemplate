using DevBridge.Templates.WebProject.DataEntities.Entities;
using FluentNHibernate.Mapping;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public sealed class AgreementMap : ClassMap<Agreement>
    {
        public AgreementMap()
        {            
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Number).Length(20).Not.Nullable();
            References(f => f.Customer)
                .Not.LazyLoad()                
                .Not.Nullable()
                .Column("CustomerId")
                .Cascade.SaveUpdate();
            Map(f => f.CreatedOn).Not.Nullable();
            Map(f => f.IsDeleted).Not.Nullable();
        }
    }
}
