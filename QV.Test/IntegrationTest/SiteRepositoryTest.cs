using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QV.Data.Models;
using QV.Service;
using QV.Service.Contracts;
using QV.Test.IntegrationTest;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace QV.Test
{
    [TestClass]
    public class SiteRepositoryTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //I use Entity Framework's DropCreateDatabaseAlways for the purpose of this demo.
            // Utility.CreateSeededTestDatabase();

          


        }

        [TestMethod]
        public void CreateSiteTest()
        {
            Site newSite;
            using (IDataContextAsync context = new Qv21Context(true))
            {
                using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
                {
                    IRepositoryAsync<Site> siteRepositoryAsync = new Repository<Site>(context, unitOfWork);
                    var site = new Site
                    {
                        Active = true,
                        Name = "IntegrationTest Site2",
                        PropertyName = "IntegrationTest PropertyName2",
                        ShortName = "IntegrationTest2"
                        // Properties = SiteProperties.RECEIVER
                    };
                    siteRepositoryAsync.Insert(site);
                    unitOfWork.SaveChanges();
                    newSite = site;
                }
            }

            //  Query for newly created site by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(context, unitOfWork);
                var site = siteRepository.Find(newSite.SiteId);
                Assert.AreEqual(site.SiteId,newSite.SiteId);
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
        //        Assert.AreEqual(dockCount,4);
        //    }
        //}


        [TestMethod]
        public void GetSiteDeepGraph()
        {

          using (IDataContextAsync context = new Qv21Context(true))
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Site> siteRepository = new Repository<Site>(context, unitOfWork);
                ISiteService siteService = new SiteService(siteRepository);
              

                var result = siteRepository
                    .Query(x => x.SiteId == 1).Include(x => x.SiteDetails).SelectAsync();

                var sites = result.Result;

                Assert.IsTrue(sites.Count() == 1);
                //Deep graph test
                var siteOne = sites.FirstOrDefault(x => x.SiteId == 1);
                Assert.IsTrue(siteOne.SiteDetails.Any());



            } 
        }
    }
}
