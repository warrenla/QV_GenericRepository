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

namespace QV.Test.IntegrationTest.WCF_Service
{
    [TestClass]
    public class WcfDockServiceTest
    {
        #region TestSetup
        public WcfDockServiceTest()
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
        #endregion

        #region Unit Test for WCF Dock Service
        [TestMethod]
        public void createDock_GenerateNewDockObject_ReturnedWithID()
        {
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Dock> repo = new Repository<Dock>(qvContext, unitOfWork);
                IWCFQvDockService dService = new QvDockService(new DockService(repo));


                var dock = new Dock()
                {
                    Active = true,
                    DockDetails = null,
                    ObjectState = ObjectState.Added,
                    Sequence = 1,
                    SiteId = 1,
                    Type = "1"
                };
                //Act
                dService.Create(dock);
                unitOfWork.SaveChanges();

                //ASSERT
                Assert.IsTrue(dock.DockId != 0);
            }
        }

        [TestMethod]
        public void readDock_LookforDock_DockReturned()
        {
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Dock> repo = new Repository<Dock>(qvContext, unitOfWork);
                IWCFQvDockService dService = new QvDockService(new DockService(repo));

                //Act
                var found = dService.Get(1);

                //Assert
                Assert.IsTrue(found.DockId == 1);
            }
        }

        [TestMethod]
        public void updateDock_GetDockAndChange_DockWithUpdatedValues()
        {
            int dockId;
            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Dock> repo = new Repository<Dock>(qvContext, unitOfWork);
                IWCFQvDockService dService = new QvDockService(new DockService(repo));

                //Get 
                Dock dock = dService.Get(1);
                dockId = dock.DockId;

                //Act
                dock.Name = "DockNameChanged";
                dService.Update(dock);
                unitOfWork.SaveChanges();
            }

            //Assert -- Check with a new context to ensure Dock info and is not being pulled from previous context in-memory.

            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Dock> repo = new Repository<Dock>(qvContext, unitOfWork);
                IWCFQvDockService dService = new QvDockService(new DockService(repo));

                //Act
                Dock dock = dService.Get(dockId);

                //Assert
                Assert.IsTrue(dock.Name == "DockNameChanged");
            }
        }

        [TestMethod]
        public void deleteDocl_GetDock_DockIsDeleted()
        {

            using (IDataContextAsync qvContext = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvContext))
            {
                //Arrange
                IRepositoryAsync<Dock> repo = new Repository<Dock>(qvContext, unitOfWork);
                IWCFQvDockService dService = new QvDockService(new DockService(repo));

                var dock = new Dock()
                {
                    Active = true,
                    DockDetails = null,
                    ObjectState = ObjectState.Added,
                    Sequence = 1,
                    SiteId = 1,
                    Type = "1"
                };


                //Act
                dService.Create(dock);
                unitOfWork.SaveChanges();

                //Get a dock
                dService.Delete(dock.DockId);
                unitOfWork.SaveChanges();

                var dockFound = dService.Get(dock.DockId);
                //Assert
                Assert.IsNull(dockFound);
            }




        }
        #endregion
        //NOTE:  I have not added any negative test, but this should be done for a production test system.  Strictly a demo here.

    }
}