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
    public class SiteDetailRepositoryTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //I use Entity Framework's DropCreateDatabaseAlways for the purpose of this demo.
            // Utility.CreateSeededTestDatabase();

        }

        [TestMethod]
        public void CreateSiteDetailTest()
        {

            int SiteDetailId;
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                var SiteDetail = new SiteDetail()
                {
                    Data = "SiteDetailData",
                    SiteId = 1,
                    Key = "SiteDetailKey",
                    ObjectState = ObjectState.Added
                };
                repositoryAsync.Insert(SiteDetail);
                unitOfWork.SaveChanges();
                SiteDetailId = SiteDetail.SiteDetailId;

            }


            //  Query for newly created item by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                var SiteDetail = repositoryAsync.Find(SiteDetailId);
                Assert.IsNotNull(SiteDetail);
            }
        }


        [TestMethod]
        public void ReadSiteDetailTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void ReadSiteDetailsTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                var results = repositoryAsync.Query().Select();
                Assert.IsNotNull(results.Count() > 1);
            }
        }

        [TestMethod]
        public void UpdateSiteDetailTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNotNull(result);
                result.SiteId = 2;
                repositoryAsync.Update(result);
                unitOfWork.SaveChanges();
            }
            // Use another context to ensure data was updated and not a im memory result
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.SiteId == 2);
            }
        }

        [TestMethod]
        public void DeleteSiteDetailTest()
        {
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                repositoryAsync.Delete(1);
                unitOfWork.SaveChanges();
            }
            // Use another context to ensure data was updated and not a im memory result
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<SiteDetail> repositoryAsync = new Repository<SiteDetail>(context, unitOfWork);
                var result = repositoryAsync.Find(1);
                Assert.IsNull(result);

            }
        }

    }
}