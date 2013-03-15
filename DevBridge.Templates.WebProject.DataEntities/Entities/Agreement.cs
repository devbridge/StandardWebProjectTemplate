namespace DevBridge.Templates.WebProject.DataEntities.Entities
{
    public class Agreement : PersistentEntityBase<Agreement>
    {
        public virtual string Number { get; set; }
        public virtual Customer Customer { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, Number: {1}, Customer: {2}", base.ToString(), Number, Customer);
        }
    }
}
