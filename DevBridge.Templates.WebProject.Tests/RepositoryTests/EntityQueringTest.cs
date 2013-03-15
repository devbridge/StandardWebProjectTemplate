using System.Linq;
using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using NUnit.Framework;

namespace DevBridge.Templates.WebProject.Tests.RepositoryTests
{
    [TestFixture]
    public class EntityQueringTest : DatabaseTestBase<int>
    {
        [Test]
        public void Should_Retrieve_Entity_Successfully()
        {
            var partResponse = TestDataProvider.CreateMultipartUpload();

            RunDatabaseActionAndAssertionsInTransaction(partResponse,
                session =>
                {
                    session.SaveOrUpdate(partResponse);
                    session.Flush();
                    session.Clear();
                },
                (responce, session) =>
                {
                    Repository repository = new Repository(session);
                    var actual = repository.AsQueryable<MultipartUpload>(f => f.UploadId == responce.UploadId).FirstOrDefault();
                    Assert.IsNotNull(actual);
                    Assert.AreEqual(partResponse, actual);
                });
        }

        [Test]
        public void Should_Retrieve_Persistent_Entity_Successfully()
        {
            var user = TestDataProvider.CreateCustomer();

            RunDatabaseActionAndAssertionsInTransaction(user,
                session =>
                {
                    session.SaveOrUpdate(user);
                    session.Flush();
                    session.Clear();
                },
                (response, session) =>
                {
                    Repository repository = new Repository(session);
                    var actual = repository.AsQueryable<Customer>(f => f.Name == response.Name && f.Code == response.Code).FirstOrDefault();
                    Assert.IsNotNull(actual);
                    Assert.AreEqual(user, actual);
                });
        }
    }
}