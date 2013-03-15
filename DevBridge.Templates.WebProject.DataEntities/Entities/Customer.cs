using System.Collections.Generic;
using DevBridge.Templates.WebProject.DataEntities.Enums;

namespace DevBridge.Templates.WebProject.DataEntities.Entities
{
    public class Customer : PersistentEntityBase<Customer>
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual CustomerType Type { get; set; }        

        public virtual IList<Agreement> Agreements { get; set; }

        public Customer()
        {
            Agreements = new List<Agreement>();
        }

        public override string ToString()
        {
            return string.Format("{0}, Name: {1}, Code: {2}, Type: {3}", Name, Code, Type);
        }
    }
}
