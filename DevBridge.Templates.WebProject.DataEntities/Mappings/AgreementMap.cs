using DevBridge.Templates.WebProject.DataEntities.Entities;
using FluentNHibernate.Mapping;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public sealed class AgreementMap : EntityMapBase<Agreement>
    {
        public AgreementMap()
        {            
            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.Number).Length(20).Not.Nullable();

            References(f => f.Customer).LazyLoad().Not.Nullable().Cascade.SaveUpdate();            
        }
    }
}
