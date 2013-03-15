namespace DevBridge.Templates.WebProject.DataEntities
{
    public abstract class EntityBase<TEntity> : IEntity<int> 
        where TEntity : class, IEntity<int>
    {
        private int? hashCode;

        public virtual int Id { get; set; }

        object IEntity.Id
        {
            get { return Id; }

            set { Id = (int) value; }
        }

        public static bool operator ==(EntityBase<TEntity> x, EntityBase<TEntity> y)
        {
            return Equals(x as TEntity, y as TEntity);
        }

        public static bool operator !=(EntityBase<TEntity> x, EntityBase<TEntity> y)
        {
            return !(x == y);
        }

        public virtual bool Equals(TEntity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (other.Id == default(int) && Id == default(int))
            {
                return ReferenceEquals(other, this);
            }

            return other.Id == Id;
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return Equals(other as TEntity);
        }

        public override int GetHashCode()
        {
            if (!hashCode.HasValue)
            {
                hashCode = Id == default(int) ? base.GetHashCode() : Id;
            }

            return hashCode.Value;
        }

        public override string ToString()
        {
            return string.Format("Type: {0}, Id: {1}", GetType().Name, Id);
        }
    }
}
