using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QV.Data;
using QV.Data.Models;
using QV.Service;
using QV.WcfServiceLibrary;
using QV.WcfServiceLibrary.Contracts;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace QV.Test.Unit_Test.Service
{
    /// <summary>
    /// An integration test.
    /// </summary>
    [TestClass]
    public class SiteServiceTest
    {
        #region TestSetup
        public SiteServiceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        #endregion

        #region Unit Test for WCF Site Service
        [TestMethod]
        public void createSite_GenerateNewSiteObject_ReturnedWithID()
        {
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(qvContext, unitOfWork);
                IWCFQvSiteService siteService = new QvSiteService(new SiteService(siteRepository));  // Can use UNITY TO AVOID THE HARD CODE HERE.
                var site = new Site() { Active = true, Docks = null, Name = "FakeTest", ObjectState = ObjectState.Added, Properties = 1, PropertyName = "FakePropertyName", ShortName = "FakeShortName", Type = "1" };

                //Act
                siteService.Create(site);
                unitOfWork.SaveChanges();

                //ASSERT
                Assert.IsTrue(site.SiteId != 0);
            }
        }

        [TestMethod]
        public void readSite_LookforSitet_SiteReturned()
        {
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(qvContext, unitOfWork);
                IWCFQvSiteService siteService = new QvSiteService(new SiteService(siteRepository));    // Can use UNITY TO AVOID THE HARD CODE HERE.

                //Act
                var siteFound = siteService.Get(1);
                //Assert
                Assert.IsTrue(siteFound.SiteId == 1);
            }
        }

        [TestMethod]
        public void updateSite_GetSiteAndChange_SiteWithUpdatedValues()
        {
            int siteId;
            // var newSite = new Site() { Active = true, Docks = null, Name = "FakeTest", ObjectState = ObjectState.Added, Properties = 1, PropertyName = "FakePropertyName", ShortName = "FakeShortName", Type = "1" };
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(qvContext, unitOfWork);
                IWCFQvSiteService siteService = new QvSiteService(new SiteService(siteRepository));    // Can use UNITY TO AVOID THE HARD CODE HERE.

                //Get a site 
                Site site = siteService.GetList().FirstOrDefault(x => x.SiteId == 1);
                siteId = site.SiteId;

                //Act
                site.Name = "SiteNameChanged";
                siteService.Update(site);
                unitOfWork.SaveChanges();
            }

            //Assert -- Check with a new context to ensure site info and is not being pulled from previous context in-memory.


            using (IDataContextAsync qv2Context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qv2Context))
            {
                //Arrange
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(qv2Context, unitOfWork);
                IWCFQvSiteService siteService = new QvSiteService(new SiteService(siteRepository));    // Can use UNITY TO AVOID THE HARD CODE HERE.

                //Act
                Site site = siteService.GetList().FirstOrDefault(x => x.SiteId == siteId);

                //Assert
                Assert.IsTrue(site.Name == "SiteNameChanged");
            }
        }

        [TestMethod]
        public void deleteSite_GetSitee_SiteIsDeleted()
        {
            int siteId;

            using (IDataContextAsync qv2Context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qv2Context))
            {
                //Arrange
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(qv2Context, unitOfWork);
                IWCFQvSiteService siteService = new QvSiteService(new SiteService(siteRepository));    // Can use UNITY TO AVOID THE HARD CODE HERE.

                //Create a site with no FK relationships
                var newSite = new Site() { Active = true, Docks = null, Name = "FakeTest", ObjectState = ObjectState.Added, Properties = 1, PropertyName = "FakePropertyName", ShortName = "FakeShortName", Type = "1" };

                //Act
                siteService.Create(newSite);
                unitOfWork.SaveChanges();

                //Get a site 
                Site site = siteService.GetList().FirstOrDefault(x => x.SiteId == newSite.SiteId);
                if (site != null)
                {
                    siteId = site.SiteId;
                    //Act
                    siteService.Delete(site.SiteId);
                    unitOfWork.SaveChanges();
                }
                var siteFound = siteService.GetList().FirstOrDefault(x => x.SiteId == newSite.SiteId);
                //Assert
                Assert.IsNull(siteFound);
            }




        }
        #endregion


        //NOTE:  I have not added any negative test, but this should be done for a production test system.  Strictly a demo here.

    }
}
