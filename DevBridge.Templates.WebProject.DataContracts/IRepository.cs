using System;
using System.Linq;
using System.Linq.Expressions;

using DevBridge.Templates.WebProject.DataEntities;

using NHibernate;

namespace DevBridge.Templates.WebProject.DataContracts
{
    public interface IRepository : IGenericRepository<int>
    {
    }

    public interface IGenericRepository<TId> where TId : struct
    {
        void Commit();

        IQueryOver<TEntity, TEntity> AsQueryOver<TEntity>(Expression<Func<TEntity>> alias = null) where TEntity : class, IEntity<TId>;

        TEntity AsProxy<TEntity>(TId id) where TEntity : class, IEntity<TId>;

        TEntity First<TEntity>(TId id) where TEntity : class, IEntity<TId>;

        TEntity First<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>;

        TEntity FirstOrDefault<TEntity>(TId id) where TEntity : class, IEntity<TId>;

        TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>;

        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : class, IEntity<TId>;

        IQueryable<TEntity> AsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>;

        bool Any<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IEntity<TId>;

        void Save<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>;

        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>;

        void Delete<TEntity>(TId id) where TEntity : class, IEntity<TId>;

        void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>;

        void Detach<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>;

        void Refresh<TEntity>(TEntity entity) where TEntity : class, IEntity<TId>;
    }
}
