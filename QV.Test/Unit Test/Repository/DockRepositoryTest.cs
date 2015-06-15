using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QV.Data.Models;
using QV.Test.Unit_Test.Fake;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace QV.Test.Unit_Test.Repository
{
    [TestClass]
    public class DockRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        #region CRUD for Dock
        /// <summary>
        /// CREATE OP
        /// </summary>
        [TestMethod]
        public void InsertDock()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Dock>()
                    .Insert(new Dock()
                    {
                        Active = true,
                        DockDetails = null,
                        DockId = 100,
                        ObjectState = ObjectState.Added,
                        Sequence = 1,
                        //  SiteId = 100,
                        Site = null,
                        Type = "1"
                    });


                unitOfWork.Repository<Dock>()
                    .Insert(new Dock()
                    {
                        Active = true,
                        DockDetails = null,
                        DockId = 200,
                        ObjectState = ObjectState.Added,
                        Sequence = 1,
                        SiteId = 100,
                        Site = null,
                        Type = "1"
                    });

                unitOfWork.Repository<Dock>()
                    .Insert(new Dock()
                    {
                        Active = true,
                        DockDetails = null,
                        DockId = 300,
                        ObjectState = ObjectState.Added,
                        Sequence = 1,
                        SiteId = 100,
                        Site = null,
                        Type = "1"
                    });


              var changes =   unitOfWork.SaveChanges();

                var dock = unitOfWork.Repository<Dock>().Find(200);
                Assert.IsNotNull(dock);
                Assert.AreEqual(200, dock.DockId);
                var siteDetaols = unitOfWork.Repository<Dock>().Query().Select();
                Assert.IsNotNull(siteDetaols);
                Assert.AreEqual(3, siteDetaols.Count());



            }
        }

        /// <summary>
        /// READE OP
        /// </summary>
        [TestMethod]
        public void FindDockById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Dock>()
                    .Insert(new Dock()
                    {
                        Active = true,
                        DockDetails = null,
                        DockId = 100,
                        ObjectState = ObjectState.Added,
                        Sequence = 1,
                        SiteId = 100,
                        Site = null,
                        Type = "1"
                    });
                unitOfWork.SaveChanges();
                var insertedSite = unitOfWork.RepositoryAsync<Dock>().Find(100);
                Assert.IsNotNull(insertedSite);



            }
        }

        /// <summary>
        /// UPDATE OP
        /// </summary>
        [TestMethod]
        public void UpdateDockById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Dock>()
                   .Insert(new Dock()
                   {
                       Active = true,
                       DockDetails = null,
                       DockId = 100,
                       ObjectState = ObjectState.Added,
                       Sequence = 1,
                       SiteId = 100,
                       Site = null,
                       Type = "1"
                   });
                unitOfWork.SaveChanges();
                var dock = unitOfWork.Repository<Dock>().Find(100);
                Assert.IsNotNull(dock);

                dock.Active = false;
                dock.ObjectState = ObjectState.Modified;

                unitOfWork.RepositoryAsync<Dock>().Update(dock);
                unitOfWork.SaveChanges();

                //Get updated product from repository
                var updatedDock = unitOfWork.RepositoryAsync<Dock>().Find(100);

                Assert.AreEqual(false, updatedDock.Active);


            }
        }

        [TestMethod]
        public void DeleteDockById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Dock>()
                   .Insert(new Dock()
                   {
                       Active = true,
                       DockDetails = null,
                       DockId = 100,
                       ObjectState = ObjectState.Added,
                       Sequence = 1,
                       SiteId = 100,
                       Site = null,
                       Type = "1"
                   });
                unitOfWork.SaveChanges();
                unitOfWork.RepositoryAsync<Dock>().Delete(100);
                var dock = unitOfWork.Repository<Dock>().Find(100);

                Assert.IsNull(dock);



            }
        }
        #endregion
    }
}
