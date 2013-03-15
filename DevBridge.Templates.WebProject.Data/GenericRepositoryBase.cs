using System;
using System.Linq;
using System.Linq.Expressions;

using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataContracts.Exceptions;
using DevBridge.Templates.WebProject.DataEntities;

using NHibernate;
using NHibernate.Linq;

namespace DevBridge.Templates.WebProject.Data
{
    public abstract class GenericRepositoryBase<TId> : IGenericRepository<TId>, IDisposable where TId : struct
    {
        private readonly ISession session;

        public GenericRepositoryBase(ISessionFactoryProvider sessionFactoryProvider)
        {
            session = sessionFactoryProvider.Open();
        }

        public GenericRepositoryBase(ISession session)
        {
            this.session = session;
        }

        public ISession Session
        {
            get
            {
                return session;
            }
        }

        public void Dispose()
        {
            session.Close();
            session.Dispose();
        }

        public void Commit()
        {
            session.Flush();
        }

        public IQueryOver<TEntity, TEntity> AsQueryOver<TEntity>(Expression<Func<TEntity>> alias = null) where TEntity : class, IEntity<TId>
        {
            if (alias != null)
            {
                return session.QueryOver(alias);
            }

            return session.QueryOver<TEntity>();
        }

        public virtual TEntity AsProxy<TEntity>(TId id) where TEntity : class, IEntity<TId>
        {
            return session.Load<TEntity>(id, LockMode.None);
        }

        public virtual TEntity First<TEntity>(TId id) where TEntity : class, IEntity<TId>
        {
            TEntity entity = FirstOrDefault<TEntity>(id);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual TEntity First<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>
        {
            TEntity entity = FirstOrDefault(filter);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), filter.ToString());
            }

            return entity;
        }

        public virtual TEntity FirstOrDefault<TEntity>(TId id) where TEntity : class, IEntity<TId>
        {
            var entity = session.Get<TEntity>(id);

            if (entity != null)
            {
                if (entity is IPersistentEntity && ((IPersistentEntity)entity).DeletedOn != null)
                {
                    return null;
                }
            }

            return entity;
        }

        public virtual TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>
        {
            return AsQueryable<TEntity>().Where(filter).FirstOrDefault();
        }

        public virtual IQueryable<TEntity> AsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>
        {
            return AsQueryable<TEntity>().Where(filter);
        }

        public virtual IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : class, IEntity<TId>
        {
            if (typeof(IPersistentEntity).IsAssignableFrom(typeof(TEntity)))
            {
                return session.Query<TEntity>().Where(f => ((IPersistentEntity)f).DeletedOn == null);
            }

            return session.Query<TEntity>();
        }

        public virtual bool Any<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>
        {
            return AsQueryable<TEntity>().Where(filter).Any();
        }

        public virtual void Save<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>
        {
            session.SaveOrUpdate(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>
        {
            session.Delete(entity);
        }

        public virtual void Delete<TEntity>(TId id) where TEntity : class, IEntity<TId>
        {
            TEntity entity = AsProxy<TEntity>(id);
            session.Delete(entity);
        }

        public virtual void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>
        {
            session.Lock(entity, LockMode.None);
        }

        public virtual void Detach<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>
        {
            session.Evict(entity);
        }

        public void Refresh<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>
        {
            session.Refresh(entity);
        }
    }
}
