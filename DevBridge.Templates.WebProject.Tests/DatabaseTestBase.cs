﻿using System;
using System.Transactions;

using Autofac;

using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataEntities;

using FluentNHibernate.Data;
using FluentNHibernate.Testing;

using NHibernate;

using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests
{
    public abstract class DatabaseTestBase<TId> : TestBase
        where TId : struct
    {
        protected void RunEntityMapTestsInTransaction<TEntity>(TEntity testEntity, Action<TEntity> resultAssertions = null) where TEntity : IEntity<TId>
        {  
            var sessionFactory = Container.Resolve<ISessionFactoryProvider>();
            using (var session = sessionFactory.Open())
            {
                using (new TransactionScope())
                {
                    var result = new PersistenceSpecification<TEntity>(session).VerifyTheMappings(testEntity);
                    if (resultAssertions != null)
                    {
                        resultAssertions(result);
                    }
                }
            }
        }

        protected void SaveEntityAndRunAssertionsInTransaction<TEntity>(
            TEntity entity, 
            Action<TEntity> resultAssertions = null, 
            Action<TEntity> assertionsBeforeSave = null, 
            Action<TEntity> assertionsAfterSave = null) where TEntity : IEntity<TId>
        {
            RunDatabaseActionAndAssertionsInTransaction(entity, 
                session => session.SaveOrUpdate(entity),
                (result, session) =>
                {
                    if (resultAssertions != null)
                    {
                        resultAssertions(result);
                    }
                },
                 (result, session) =>
                 {
                     if (assertionsBeforeSave != null)
                     {
                         assertionsBeforeSave(result);
                     }
                 },
                (result, session) =>
                {
                    if (assertionsAfterSave != null)
                    {
                        assertionsAfterSave(result);
                    }
                });
        }

        protected void DeleteCreatedEntityAndRunAssertionsInTransaction<TEntity>(
            TEntity entity,
            Action<TEntity> resultAssertions = null,
            Action<TEntity> assertionsBeforeSave = null,
            Action<TEntity> assertionsAfterSave = null) where TEntity : IEntity<TId>
        {
            RunDatabaseActionAndAssertionsInTransaction(entity, session =>
                {
                    session.SaveOrUpdate(entity);
                    session.Flush();
                    session.Delete(entity);
                    session.Flush();
                },
                (result, session) =>
                    {
                        if (resultAssertions != null)
                        {
                            resultAssertions(result);
                        }
                    },
                (result, session) =>
                    {
                        if (assertionsBeforeSave != null)
                        {
                            assertionsBeforeSave(result);
                        }
                    },
                (result, session) =>
                    {
                        if (assertionsAfterSave != null)
                        {
                            assertionsAfterSave(result);
                        }
                    });
        }

        protected void RunDatabaseActionAndAssertionsInTransaction<TEntity>(TEntity entity, 
            Action<ISession> databaseAction = null,
            Action<TEntity, ISession> resultAssertions = null, 
            Action<TEntity, ISession> assertionsBeforeSave = null,
            Action<TEntity, ISession> assertionsAfterSave = null) where TEntity : IEntity<TId>
        {
            if (databaseAction == null)
            {
                Assert.Fail("No database action specified.");
            }

            if (resultAssertions == null && assertionsBeforeSave == null && assertionsAfterSave == null) 
            {
                Assert.Fail("No assertion specified!");
            }

            var sessionFactory = Container.Resolve<ISessionFactoryProvider>();

            using (var session = sessionFactory.Open())
            {
                using (new TransactionScope())
                {
                    if (assertionsBeforeSave != null)
                    {
                        assertionsBeforeSave(entity, session);
                    }

                    databaseAction(session);

                    if (assertionsAfterSave != null)
                    {
                        assertionsAfterSave(entity, session);
                    }

                    session.Flush();
                    session.Clear();
                    TEntity result = session.Get<TEntity>(entity.Id);

                    if (resultAssertions != null)
                    {
                        resultAssertions(result, session);
                    }
                }
            }
        }

        protected void RunActionInTransaction(Action<ISession> actionInTransaction)
        {
            if (actionInTransaction == null)
            {
                Assert.Fail("No action specified.");
            }

            var sessionFactory = Container.Resolve<ISessionFactoryProvider>();

            using (var session = sessionFactory.Open())
            {
                using (new TransactionScope())
                {
                    actionInTransaction(session);         
                }
            }
        }
    }
}
