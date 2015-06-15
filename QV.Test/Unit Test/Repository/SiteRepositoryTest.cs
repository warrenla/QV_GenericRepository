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
    public class SiteRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        #region CRUD for Site
        /// <summary>
        /// CREATE OP
        /// </summary>
        [TestMethod]
        public void InsertSite()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site>()
                    .Insert(new Site()
                    {
                        SiteId = 100,
                        Active = true,
                        Docks = null,
                        Name = "FakeTest1",
                        ObjectState = ObjectState.Added,
                        Properties = 1,
                        PropertyName = "FakePropertyName1",
                        ShortName = "FakeShortName100",
                        SiteDetails = null,
                        Type = "1"
                    });
                unitOfWork.Repository<Site>()
                    .Insert(new Site()
                    {
                        SiteId = 200,
                        Active = true,
                        Docks = null,
                        Name = "FakeTest2",
                        ObjectState = ObjectState.Added,
                        Properties = 1,
                        PropertyName = "FakePropertyName2",
                        ShortName = "FakeShortName200",
                        SiteDetails = null,
                        Type = "12"
                    });
                unitOfWork.Repository<Site>()
                    .Insert(new Site()
                    {
                        SiteId = 300,
                        Active = true,
                        Docks = null,
                        Name = "FakeTest3",
                        ObjectState = ObjectState.Added,
                        Properties = 1,
                        PropertyName = "FakePropertyName3",
                        ShortName = "FakeShortName300",
                        SiteDetails = null,
                        Type = "1"
                    });

                unitOfWork.SaveChanges();

                var site = unitOfWork.Repository<Site>().Find(200);
                Assert.IsNotNull(site);
                Assert.AreEqual(200, site.SiteId);
                var sites = unitOfWork.Repository<Site>().Query().Select();
                Assert.IsNotNull(sites);
                Assert.AreEqual(3, sites.Count());



            }
        }

        /// <summary>
        /// READE OP
        /// </summary>
        [TestMethod]
        public void FindSiteById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site>().Insert(new Site()
                {
                    SiteId = 100,
                    Active = true,
                    Docks = null,
                    Name = "FakeTest",
                    ObjectState = ObjectState.Added,
                    Properties = 1,
                    PropertyName = "FakePropertyName",
                    ShortName = "FakeShortName",
                    SiteDetails = null,
                    Type = "1"

                });
                unitOfWork.SaveChanges();
                var insertedSite = unitOfWork.RepositoryAsync<Site>().Find(100);
                Assert.IsNotNull(insertedSite);



            }
        }

        /// <summary>
        /// UPDATE OP
        /// </summary>
        [TestMethod]
        public void UpdateSiteById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site>().Insert(new Site()
                {
                    SiteId = 100,
                    Active = true,
                    Docks = null,
                    Name = "FakeTest",
                    ObjectState = ObjectState.Added,
                    Properties = 1,
                    PropertyName = "FakePropertyName",
                    ShortName = "FakeShortName",
                    SiteDetails = null,
                    Type = "1"

                });
                unitOfWork.SaveChanges();
                var site = unitOfWork.Repository<Site>().Find(100);
                Assert.IsNotNull(site);

                site.Active = false;
                site.ObjectState = ObjectState.Modified;

                unitOfWork.RepositoryAsync<Site>().Update(site);
                unitOfWork.SaveChanges();

                //Get updated product from repository
                var updatedSite = unitOfWork.RepositoryAsync<Site>().Find(100);

                Assert.AreEqual(site.Active = false, updatedSite.Active);


            }
        }

        [TestMethod]
        public void DeleteSiteById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site>().Insert(new Site()
                {
                    SiteId = 100,
                    Active = true,
                    Docks = null,
                    Name = "FakeTest",
                    ObjectState = ObjectState.Added,
                    Properties = 1,
                    PropertyName = "FakePropertyName",
                    ShortName = "FakeShortName",
                    SiteDetails = null,
                    Type = "1"

                });
                unitOfWork.SaveChanges();
                unitOfWork.RepositoryAsync<Site>().Delete(100);
                var site = unitOfWork.Repository<Site>().Find(100);

                Assert.IsNull(site);



            }
        }
        #endregion
        
        [TestMethod]
        public void SiteDeepGraphLoad()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                var newSite = new Site(){SiteId = 100,Active = true,Docks = null,Name = "FakeTest",ObjectState = ObjectState.Added,Properties = 1,PropertyName = "FakePropertyName",ShortName = "FakeShortName",Type = "1"};
                unitOfWork.Repository<Site>().Insert(newSite);
                // Add a site detail to a site and link
                var siteDetail = new SiteDetail()
                {
                    Data = "Alpha, Beta",
                    Key = "l333",
                    ObjectState = ObjectState.Added,
                    SiteId = newSite.SiteId,
                    SiteDetailId = 100
                };
                unitOfWork.Repository<SiteDetail>().Insert(siteDetail);
                
                // Add a Dock and link to a site
                var newDock = new Dock(){Active = true,DockDetails = null,DockId = 100,ObjectState = ObjectState.Added,Sequence = 1,SiteId = newSite.SiteId,Type = "1"};
                unitOfWork.Repository<Dock>().Insert(newDock);
                
                unitOfWork.SaveChanges();

                //test to see if repository loaded detail
                var result     = unitOfWork.Repository<SiteDetail>().Find(siteDetail.SiteDetailId);
                Assert.AreEqual(siteDetail.SiteDetailId,result.SiteDetailId);
            }
        }



    }
}