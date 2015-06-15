using Microsoft.VisualStudio.TestTools.UnitTesting;
using QV.Data.Models;
using QV.Service;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace QV.Test.IntegrationTest
{
    [TestClass]
    public class DockRepositoryTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //I use Entity Framework's DropCreateDatabaseAlways for the purpose of this demo.
            // Utility.CreateSeededTestDatabase();

        }

        [TestMethod]
        public void CreateDockTest()
        {

            Dock newDock;
            Site newSite;
            using (IDataContextAsync context = new Qv21Context(true))
            {
                using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
                {
                    IRepositoryAsync<Site> siteRepositoryAsync = new Repository<Site>(context, unitOfWork);
                    var site = new Site
                    {
                        Active = true,
                        Name = "IntegrationTest Site",
                        PropertyName = "IntegrationTest PropertyName",
                        ShortName = "IntegrationTest"
                        // Properties = SiteProperties.RECEIVER
                    };
                    siteRepositoryAsync.Insert(site);
                    unitOfWork.SaveChanges();

                    IRepositoryAsync<Dock> dockRepositoryAsync = new Repository<Dock>(context, unitOfWork);
                    var dock = new Dock()
                    {
                        Active = true,
                        DockDetails = null,
                        DockId = 300,
                        ObjectState = ObjectState.Added,
                        Sequence = 1,
                        SiteId = site.SiteId,
                        Site = site,
                        Type = "1"
                    };
                    dockRepositoryAsync.Insert(dock);
                    unitOfWork.SaveChanges();
                    newDock = dock;
                }
            }

            //  Query for newly created site by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(context, unitOfWork);
                var dock = siteRepository.Find(newDock.DockId);
                Assert.AreEqual(newDock.DockId, newDock.DockId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
        public void CreateInvalidDockTest()
        {
            Dock newDock;
            Site newSite;
            using (IDataContextAsync context = new Qv21Context(true))
            {
                using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
                {

                    IRepositoryAsync<Dock> dockRepositoryAsync = new Repository<Dock>(context, unitOfWork);
                    var dock = new Dock()
                    {
                        Active = true,
                        DockDetails = null,
                        DockId = 300,
                        ObjectState = ObjectState.Added,
                        Sequence = 1,
                        SiteId = 10000,  //Invalid SiteID
                        Type = "1"
                    };
                    dockRepositoryAsync.Insert(dock);
                    unitOfWork.SaveChanges();
                    newDock = dock;
                }
            }

            //  Query for newly created site by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(context, unitOfWork);
                var dock = siteRepository.Find(newDock.DockId);
                Assert.AreEqual(newDock.DockId, newDock.DockId);
            }
        }

        //[TestMethod]
        //public void SiteDockCountTest()
        //{
        //    using (IDataContextAsync context = new Qv21Context(true))
        //    using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
        //    {
        //        IRepositoryAsync<Site> siteRepository = new Repository<Site>(context, unitOfWork);
        //        ISiteService siteService = new SiteService(siteRepository);
        //        int dockCount = siteService.GetSiteDockCount(1);
        //        Assert.AreEqual(dockCount, 4);
        //    }
        //}



    }
}
