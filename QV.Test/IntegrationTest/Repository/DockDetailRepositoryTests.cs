using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QV.Data;
using QV.Data.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace QV.Test.IntegrationTest
{
    [TestClass]
    public class DockDetailRepositoryTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //I use Entity Framework's DropCreateDatabaseAlways for the purpose of this demo.
            // Utility.CreateSeededTestDatabase();

        }

        [TestMethod]
        public void CreateDockDetailTest()
        {

            int dockDetailId;
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                var dockDetail = new DockDetail()
                {
                    Data = "dockDetailData",
                    DockId = 1,
                    Key = "DockDetailKey",
                    ObjectState = ObjectState.Added
                };
                repositoryAsync.Insert(dockDetail);
                unitOfWork.SaveChanges();
                dockDetailId = dockDetail.DockDetailId;

            }


            //  Query for newly created item by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                var dockDetail = repositoryAsync.Find(dockDetailId);
                Assert.IsNotNull(dockDetail);
            }
        }


        [TestMethod]
        public void ReadDockDetailTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void ReadDockDetailsTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                var results = repositoryAsync.Query().Select();
                Assert.IsNotNull(results.Count() > 1);
            }
        }

        [TestMethod]
        public void UpdateDockDetailTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNotNull(result);
                result.DockId = 2;
                repositoryAsync.Update(result);
                unitOfWork.SaveChanges();
            }
            // Use another context to ensure data was updated and not a im memory result
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.DockId == 2);
            }
        }

        [TestMethod]
        public void DeleteDockDetailTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                repositoryAsync.Delete(1);
                unitOfWork.SaveChanges();
            }
            // Use another context to ensure data was updated and not a im memory result
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<DockDetail> repositoryAsync = new Repository<DockDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNull(result);

            }
        }

    }
}