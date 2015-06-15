using Microsoft.VisualStudio.TestTools.UnitTesting;
using QV.Data.Models;
using QV.Service;
using QV.WcfServiceLibrary;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace QV.Test.IntegrationTest.WCF_Service
{
    [TestClass]
    public class WcfSiteDetailServiceTest
    {
        public WcfSiteDetailServiceTest()
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

        #region Dock Service Test
        [TestMethod]
        public void createSiteDetail_GenerateNewSiteDetailObject_ReturnedWithID()
        {
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<SiteDetail> repo = new Repository<SiteDetail>(qvContext, unitOfWork);

                IWCFQvSiteDetailService dService = new QvSiteDetailService(new SiteDetailService(repo));

                var siteDetail = new Data.Models.SiteDetail()
                {
                    Data = "Alpha, Beta",
                    Key = "l333",
                    ObjectState = ObjectState.Added,
                    SiteId = 1

                };

                //Act
                dService.Create(siteDetail);
                unitOfWork.SaveChanges();

                //ASSERT
                Assert.IsTrue(siteDetail.SiteDetailId != 0);
            }
        }

        [TestMethod]
        public void readSiteDetail_LookforSiteDetail_SiteDetailReturned()
        {
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<SiteDetail> repo = new Repository<SiteDetail>(qvContext, unitOfWork);

                IWCFQvSiteDetailService dService = new QvSiteDetailService(new SiteDetailService(repo));


                //Act
                var found = dService.Get(1);
                //Assert
                Assert.IsTrue(found.SiteDetailId == 1);
            }
        }

        [TestMethod]
        public void updateSiteDetail_GetSiteDetailAndChange_SiteDetailWithUpdatedValues()
        {
            int Id;
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<SiteDetail> repo = new Repository<SiteDetail>(qvContext, unitOfWork);
                IWCFQvSiteDetailService dService = new QvSiteDetailService(new SiteDetailService(repo));

                //Get 
                var siteDetail = dService.Get(1);
                Id = siteDetail.SiteDetailId;

                //Act
                siteDetail.SiteId= 2;
                dService.Update(siteDetail);
                unitOfWork.SaveChanges();
            }

            //Assert 
            //      Double check    - with a new context check to ensure site info changed in storage.

            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<SiteDetail> repo = new Repository<SiteDetail>(qvContext, unitOfWork);
                IWCFQvSiteDetailService dService = new QvSiteDetailService(new SiteDetailService(repo));

                //Get 
                SiteDetail siteDetail = dService.Get(Id);
               
                //Assert
                Assert.IsTrue(siteDetail.SiteId == 2);
            }
        }

        [TestMethod]
        public void deleteSiteDetail_GetSiteDetail_SiteDetailIsDeleted()
        {

            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<SiteDetail> repo = new Repository<SiteDetail>(qvContext, unitOfWork);

                IWCFQvSiteDetailService dService = new QvSiteDetailService(new SiteDetailService(repo));
                var siteDetail = new Data.Models.SiteDetail()
                {
                    Data = "Alpha, Beta",
                    Key = "l333",
                    ObjectState = ObjectState.Added,
                    SiteId = 1

                };

                //Act
                dService.Create(siteDetail);
                unitOfWork.SaveChanges();

               
                //Get a dock
                dService.Delete(siteDetail.SiteDetailId);
                unitOfWork.SaveChanges();

                var siteDetailFound = dService.Get(siteDetail.SiteDetailId);
                //Assert
                Assert.IsNull(siteDetailFound);
            }




        }
        #endregion
        //NOTE:  I have not added any negative test, but this should be done for a production test system.  Strictly a demo here.

    }
}