using System;
using System.Linq;
using System.Linq.Expressions;
using DevBridge.Templates.WebProject.DataEntities;

namespace DevBridge.Templates.WebProject.DataContracts
{ 
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        TEntity First(int id);

        TEntity First(Expression<Func<TEntity, bool>> filter);

        TEntity FirstOrDefault(int id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter);

        TEntity CreateProxy(int id);

        IQueryable<TEntity> AsQueryable();

        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter);

        bool Any(Expression<Func<TEntity, bool>> filter);

        void Save(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);
    }
}
