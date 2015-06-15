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
    public class SiteDetailRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        #region CRUD for SiteDetail
        /// <summary>
        /// CREATE OP
        /// </summary>
        [TestMethod]
        public void InsertSiteDetail()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.SiteDetail>()
                    .Insert(new Site.SiteDetail()
                    {
                        Data = "Alpha, Beta",
                        Key = "l333",

                        ObjectState = ObjectState.Added,
                        SiteId = 100,
                        SiteDetailId = 100
                    });
                unitOfWork.Repository<Site.SiteDetail>()
                     .Insert(new Site.SiteDetail()
                     {
                         Data = "Alpha, Beta",
                         Key = "l4444",
                         ObjectState = ObjectState.Added,
                         SiteId = 100,
                         SiteDetailId = 200
                     });
                unitOfWork.Repository<Site.SiteDetail>()
                     .Insert(new Site.SiteDetail()
                     {
                         Data = "Alpha, Beta",
                         Key = "l5532",
                         ObjectState = ObjectState.Added,
                         SiteId = 100,
                         SiteDetailId = 300
                     });

                unitOfWork.SaveChanges();

                var siteDetail = unitOfWork.Repository<Site.SiteDetail>().Find(200);
                Assert.IsNotNull(siteDetail);
                Assert.AreEqual(200, siteDetail.SiteDetailId);
                var siteDetaols = unitOfWork.Repository<Site.SiteDetail>().Query().Select();
                Assert.IsNotNull(siteDetaols);
                Assert.AreEqual(3, siteDetaols.Count());



            }
        }

        /// <summary>
        /// READE OP
        /// </summary>
        [TestMethod]
        public void FindSiteDetailById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.SiteDetail>()
                    .Insert(new Site.SiteDetail()
                    {
                        Data = "Alpha, Beta",
                        Key = "l333",
                        ObjectState = ObjectState.Added,
                        SiteId = 100,
                        SiteDetailId = 100
                    });
                unitOfWork.SaveChanges();
                var insertedSite = unitOfWork.RepositoryAsync<Site.SiteDetail>().Find(100);
                Assert.IsNotNull(insertedSite);



            }
        }

        /// <summary>
        /// UPDATE OP
        /// </summary>
        [TestMethod]
        public void UpdateSiteDetailById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.SiteDetail>()
                    .Insert(new Site.SiteDetail()
                    {
                        Data = "Alpha, Beta",
                        Key = "l333",
                        ObjectState = ObjectState.Added,
                        SiteId = 100,
                        SiteDetailId = 100
                    });
                unitOfWork.SaveChanges();
                var siteDetail = unitOfWork.Repository<Site.SiteDetail>().Find(100);
                Assert.IsNotNull(siteDetail);

                siteDetail.Data = "Updated";
                siteDetail.ObjectState = ObjectState.Modified;

                unitOfWork.RepositoryAsync<Site.SiteDetail>().Update(siteDetail);
                unitOfWork.SaveChanges();

                //Get updated product from repository
                var updatedSiteDetail = unitOfWork.RepositoryAsync<Site.SiteDetail>().Find(100);

                Assert.AreEqual(siteDetail.Data, updatedSiteDetail.Data);


            }
        }

        [TestMethod]
        public void DeleteSiteDetailById()
        {
            using (IDataContextAsync qvFakeContext = new QvFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(qvFakeContext))
            {
                unitOfWork.Repository<Site.SiteDetail>()
                     .Insert(new Site.SiteDetail()
                     {
                         Data = "Alpha, Beta",
                         Key = "l333",
                         ObjectState = ObjectState.Added,
                         SiteId = 100,
                         SiteDetailId = 100
                     });
                unitOfWork.SaveChanges();
                unitOfWork.RepositoryAsync<Site.SiteDetail>().Delete(100);
                var siteDetail = unitOfWork.Repository<Site.SiteDetail>().Find(100);

                Assert.IsNull(siteDetail);



            }
        }
        #endregion
    }
}
