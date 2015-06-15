using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QV.Test.Unit_Test.Fake;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using Site = QV.Data.Models;

namespace QV.Test.Unit_Test.Repository
{
    [TestClass]
    public class DockDetailRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        #region CRUD for DockDetail
        /// <summary>
        /// CREATE OP
        /// </summary>
        [TestMethod]
        public void InsertDockDetail()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.DockDetail>()
                    .Insert(new Site.DockDetail() { Data = "TestData", Dock = null, DockDetailId = 100, Key = "TestKey", ObjectState = ObjectState.Added, DockId = 100 });
                unitOfWork.Repository<Site.DockDetail>()
                   .Insert(new Site.DockDetail() { Data = "TestData", Dock = null, DockDetailId = 200, Key = "TestKey", ObjectState = ObjectState.Added, DockId = 100 });

                unitOfWork.Repository<Site.DockDetail>()
                   .Insert(new Site.DockDetail() { Data = "TestData", Dock = null, DockDetailId = 300, Key = "TestKey", ObjectState = ObjectState.Added, DockId = 300 });
                unitOfWork.SaveChanges();

                var dockDetail = unitOfWork.Repository<Site.DockDetail>().Find(200);
                Assert.IsNotNull(dockDetail);
                Assert.AreEqual(200, dockDetail.DockDetailId);
                var siteDetaols = unitOfWork.Repository<Site.DockDetail>().Query().Select();
                Assert.IsNotNull(siteDetaols);
                Assert.AreEqual(3, siteDetaols.Count());



            }
        }

        /// <summary>
        /// READE OP
        /// </summary>
        [TestMethod]
        public void FindDockDetailById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.DockDetail>()
                .Insert(new Site.DockDetail() { Data = "TestData", Dock = null, DockDetailId = 100, Key = "TestKey", ObjectState = ObjectState.Added, DockId = 100 });
                unitOfWork.SaveChanges();
                var insertedSite = unitOfWork.RepositoryAsync<Site.DockDetail>().Find(100);
                Assert.IsNotNull(insertedSite);



            }
        }

        /// <summary>
        /// UPDATE OP
        /// </summary>
        [TestMethod]
        public void UpdateDockDetailById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.DockDetail>()
                .Insert(new Site.DockDetail() { Data = "TestData", Dock = null, DockDetailId = 100, Key = "TestKey", ObjectState = ObjectState.Added, DockId = 100 });
                unitOfWork.SaveChanges();
                var dockDetail = unitOfWork.Repository<Site.DockDetail>().Find(100);
                Assert.IsNotNull(dockDetail);

                dockDetail.Data = "Updated";
                dockDetail.ObjectState = ObjectState.Modified;

                unitOfWork.RepositoryAsync<Site.DockDetail>().Update(dockDetail);
                unitOfWork.SaveChanges();

                //Get updated product from repository
                var updatedDockDetail = unitOfWork.RepositoryAsync<Site.DockDetail>().Find(100);

                Assert.AreEqual(dockDetail.Data, updatedDockDetail.Data);


            }
        }

        [TestMethod]
        public void DeleteDockDetailById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.DockDetail>()
                .Insert(new Site.DockDetail() { Data = "TestData", Dock = null, DockDetailId = 100, Key = "TestKey", ObjectState = ObjectState.Added, DockId = 100 });
                unitOfWork.SaveChanges();
                unitOfWork.RepositoryAsync<Site.DockDetail>().Delete(100);
                var dockDetail = unitOfWork.Repository<Site.DockDetail>().Find(100);

                Assert.IsNull(dockDetail);



            }
        }
        #endregion
    }
}
