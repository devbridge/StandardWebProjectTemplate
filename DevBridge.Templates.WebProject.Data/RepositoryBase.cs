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
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>, IUnitOfWorkRepository
        where TEntity : class, IEntity 
    {
        private IUnitOfWork unitOfWork;

        private IUnitOfWork UnitOfWork
        {
            get
            {                
                if (unitOfWork == null)
                {
                    throw new DataException(string.Format("Repository {0} has no assigned unit of work.", GetType().Name));
                }
                return unitOfWork;
            }
        }

        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Use(IUnitOfWork unitOfWorkToUse)
        {
            unitOfWork = unitOfWorkToUse;
        }

        public TEntity First(int id)
        {
            TEntity entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            return entity;
        }

        public TEntity First(Expression<Func<TEntity, bool>> filter)
        {
            TEntity entity = FirstOrDefault(filter);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), filter.ToString());
            }
            return entity;
        }

        public TEntity FirstOrDefault(int id)
        {
            return AsQueryable().FirstOrDefault(f => f.Id == id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return AsQueryable().FirstOrDefault(filter);
        }

        public TEntity CreateProxy(int id)
        {
            return UnitOfWork.Session.Load<TEntity>(id, LockMode.None);
        }

        public IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return AsQueryable().Where(filter);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return UnitOfWork.Session.Query<TEntity>().Where(f => f.DeletedOn == null);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return AsQueryable().Count(filter) > 0;
        }

        public void Save(TEntity entity)
        {
            UnitOfWork.Session.SaveOrUpdate(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.DeletedOn = DateTime.Now;
            UnitOfWork.Session.SaveOrUpdate(entity);
        }

        public void Delete(int id)
        {            
            TEntity entity = First(id);            
            Delete(entity);
        }
    }
}