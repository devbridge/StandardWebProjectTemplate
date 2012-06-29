using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.DataContracts.Exceptions;
using DevBridge.Templates.WebProject.DataEntities;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.Tests.TestHelpers;
using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.DataContracts
{
    [TestFixture]
    public class GenericRepositoryTest
    {
        private void ShouldLoadTop10EntitiesWithAllMethodSuccessfully<TEntity>(IRepository<TEntity> repository)
            where TEntity : IEntity
        {
            string repositoryMessage = string.Format("Failed in {0}.", repository.GetType().Name);

            var list = repository.AsQueryable().Take(10).ToList();
            Assert.IsNotNull(list, repositoryMessage);
            Assert.Greater(list.Count(), 0, repositoryMessage);
            Assert.IsFalse(list.Any(f => f.DeletedOn != null), repositoryMessage);
        }

        [Test]
        public void Should_Load_Top_10_Entities_With_All_Method_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                ShouldLoadTop10EntitiesWithAllMethodSuccessfully(new AgreementRepository(unitOfWork));
                ShouldLoadTop10EntitiesWithAllMethodSuccessfully(new CustomerRepository(unitOfWork));
            }
        }

        private void ShouldGetAnyEntitySuccessfully<TEntity>(IRepository<TEntity> repository) where TEntity : IEntity
        {
            string repositoryMessage = string.Format("Failed in {0}.", repository.GetType().Name);
            TEntity entity = repository.AsQueryable().FirstOrDefault();
            Assert.IsNotNull(entity, repositoryMessage + " Table is empty.");
            TEntity entityViaGet = repository.FirstOrDefault(entity.Id);
            Assert.IsNotNull(entityViaGet);
            Assert.AreEqual(entity, entityViaGet);
        }

        [Test]
        public void Should_Get_Any_Entity_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                ShouldGetAnyEntitySuccessfully(new AgreementRepository(unitOfWork));
                ShouldGetAnyEntitySuccessfully(new CustomerRepository(unitOfWork));
            }
        }

        private void ShouldFindAnyEntityByFilterSuccessfully<TEntity>(IRepository<TEntity> repository)
            where TEntity : IEntity
        {
            string repositoryMessage = string.Format("Failed in {0}.", repository.GetType().Name);
            TEntity entity = repository.AsQueryable().FirstOrDefault();
            Assert.IsNotNull(entity, repositoryMessage + " Table is empty.");
            TEntity entityViaFindBy = repository.FirstOrDefault(f => f.Id == entity.Id);
            Assert.IsNotNull(entityViaFindBy);
            Assert.AreEqual(entity, entityViaFindBy);
        }

        [Test]
        public void Should_Find_Any_Entity_By_Filter_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                ShouldFindAnyEntityByFilterSuccessfully(new AgreementRepository(unitOfWork));
                ShouldFindAnyEntityByFilterSuccessfully(new CustomerRepository(unitOfWork));
            }
        }

        private void ShouldFilterEntitiesByFilterSuccessfully<TEntity>(IRepository<TEntity> repository)
            where TEntity : IEntity
        {
            string repositoryMessage = string.Format("Failed in {0}.", repository.GetType().Name);
            Expression<Func<TEntity, bool>> filter = (f => f.Id > 0);

            IList<TEntity> entities = repository.AsQueryable().Where(filter).ToList();
            Assert.IsNotNull(entities, repositoryMessage + " Table is empty.");
            Assert.Greater(entities.Count, 0, repositoryMessage + " Table is empty.");

            IList<TEntity> entitieViaFilterBy = repository.AsQueryable(filter).ToList();
            Assert.IsNotNull(entitieViaFilterBy);

            Assert.AreEqual(entities.Count, entitieViaFilterBy.Count);
            for (int i = 0; i < entities.Count; i++)
            {
                Assert.AreEqual(entities[i], entitieViaFilterBy.FirstOrDefault(f => f.Id == entities[i].Id),
                                repositoryMessage);
            }
        }

        [Test]
        public void Should_Filter_Entities_By_Filter_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                ShouldFilterEntitiesByFilterSuccessfully(new AgreementRepository(unitOfWork));
                ShouldFilterEntitiesByFilterSuccessfully(new CustomerRepository(unitOfWork));
            }
        }

        private void ShouldCheckEntityForExistenceSuccessfully<TEntity>(IRepository<TEntity> repository)
            where TEntity : IEntity
        {
            string repositoryMessage = string.Format("Failed in {0}.", repository.GetType().Name);
            TEntity entity = repository.AsQueryable().FirstOrDefault();
            Assert.IsNotNull(entity, repositoryMessage + " Table is empty.");
            bool entityExist = repository.Any(f => f.Id == entity.Id);
            Assert.IsTrue(entityExist, repositoryMessage);
            bool entityNotExist = repository.Any(f => f.Id == -1);
            Assert.IsFalse(entityNotExist, repositoryMessage);
        }

        [Test]
        public void Should_Check_Entity_For_Existence_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                ShouldCheckEntityForExistenceSuccessfully(new AgreementRepository(unitOfWork));
                ShouldCheckEntityForExistenceSuccessfully(new CustomerRepository(unitOfWork));
            }
        }

        private void ShouldSaveUpdateAndDeleteEntitySuccessfully<TEntity>(IUnitOfWork unitOfWork,
                                                                          IRepository<TEntity> repository,
                                                                          TEntity randomEntity) where TEntity : IEntity
        {
            string repositoryMessage = string.Format("Failed in {0}.", repository.GetType().Name);
                
            // Save new entity.
            repository.Save(randomEntity);
            unitOfWork.Session.Flush();

            TEntity entityFromDatabase = repository.FirstOrDefault(randomEntity.Id);
            Assert.IsNotNull(entityFromDatabase, repositoryMessage);
            Assert.AreEqual(randomEntity, entityFromDatabase, repositoryMessage);

            // Update entity.
            randomEntity.CreatedOn = Singleton.TestDataProvider.ProvideRandomDateTime();
            repository.Save(randomEntity);
            unitOfWork.Session.Flush();

            entityFromDatabase = repository.FirstOrDefault(randomEntity.Id);
            Assert.IsNotNull(entityFromDatabase, repositoryMessage);
            Assert.AreEqual(randomEntity, entityFromDatabase, repositoryMessage);

            // Delete entity.
            repository.Delete(randomEntity.Id);
            unitOfWork.Session.Flush();

            entityFromDatabase = repository.FirstOrDefault(randomEntity.Id);
            Assert.IsNull(entityFromDatabase, repositoryMessage);
        }

        [Test]
        public void Should_Save_Update_And_Delete_Customer_Entity_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    Customer newCustomer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                    ShouldSaveUpdateAndDeleteEntitySuccessfully(unitOfWork, new CustomerRepository(unitOfWork), newCustomer);
                }
                finally
                {
                    unitOfWork.Rollback();
                }
            }
        }

        [Test]
        public void Should_Save_Update_And_Delete_Agreement_Entity_Successfully()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    Customer newCustomer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                    Agreement newAgreement = Singleton.TestDataProvider.CreateNewRandomAgreementForCustomer(newCustomer);
                    ShouldSaveUpdateAndDeleteEntitySuccessfully(unitOfWork, new AgreementRepository(unitOfWork), newAgreement);
                }
                finally
                {
                    unitOfWork.Rollback();
                }
            }
        }

        [Test]
        public void Check_Disposed_UnitOfWork()
        {
            ICustomerRepository customerRepository;
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {                
                customerRepository = new CustomerRepository(unitOfWork);                
            }

            Assert.Catch<ObjectDisposedException>(delegate
                                                      {
                                                          customerRepository.Any(f => f.Id > 0);
                                                      });                                                       
        }

        [Test]
        public void Check_Entity_Not_Found_Exception()
        {
            ICustomerRepository customerRepository;
            using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
            {
                customerRepository = new CustomerRepository(unitOfWork);
                Assert.Catch<EntityNotFoundException>(delegate
                                                          {
                                                              customerRepository.First(-1);
                                                          });

                Assert.Catch<EntityNotFoundException>(delegate
                                                          {
                                                              customerRepository.First(
                                                                  f => f.Name == Guid.NewGuid().ToString());
                                                          });
            }
        }

        [Test]
        public void Should_Insert_And_Update_In_Separetad_Unit_Of_Works()
        {
            Customer customer = null;
            try
            {
                customer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    customerRepository.Save(customer);
                    unitOfWork.Commit();
                }

                string updatedName = Singleton.TestDataProvider.ProvideRandomString(50);
                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    Customer customer2 = customerRepository.FirstOrDefault(customer.Id);
                    customer2.Name = updatedName;
                    customerRepository.Save(customer2);
                    unitOfWork.Commit();                    
                }

                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    var updatedCustomer = customerRepository.AsQueryable(f => f.Name == updatedName).FirstOrDefault();
                    Assert.IsNotNull(updatedCustomer);
                    Assert.AreEqual(customer.Code, updatedCustomer.Code);
                }
            }
            finally
            {
                if (customer != null)
                {
                    using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                    {
                        ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                        customerRepository.Delete(customer.Id);
                        unitOfWork.Commit();                        
                    }
                }
            }
        }

        [Test]
        public void Should_Insert_And_Update_In_Separetad_Transactions()
        {
            Customer customer = null;
            try
            {
                customer = Singleton.TestDataProvider.CreateNewRandomCustomer();
                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    unitOfWork.BeginTransaction();

                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    customerRepository.Save(customer);
                    unitOfWork.Commit();
                }

                string updatedName = Singleton.TestDataProvider.ProvideRandomString(50);
                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    unitOfWork.BeginTransaction();

                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    Customer customer2 = customerRepository.FirstOrDefault(customer.Id);
                    customer2.Name = updatedName;
                    customerRepository.Save(customer2);
                    unitOfWork.Commit();
                }

                using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                {
                    ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                    var updatedCustomer = customerRepository.AsQueryable(f => f.Name == updatedName).FirstOrDefault();
                    Assert.IsNotNull(updatedCustomer);
                    Assert.AreEqual(customer.Code, updatedCustomer.Code);
                }
            }
            finally
            {
                if (customer != null)
                {
                    using (IUnitOfWork unitOfWork = new UnitOfWork(Singleton.SessionFactoryProvider))
                    {
                        ICustomerRepository customerRepository = new CustomerRepository(unitOfWork);
                        customerRepository.Delete(customer.Id);
                        unitOfWork.Commit();
                    }
                }
            }
        }       
    }
}

