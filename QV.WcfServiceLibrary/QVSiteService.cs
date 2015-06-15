using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;
using QV.Service;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace QV.WcfServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class QvSiteService : IWCFQvSiteService
    {
        private readonly ISiteService _siteService;




        // private readonly IUnitOfWorkAsync _unitOfWork;

        //public QvService()
        //{
        //    // I did not have time to implement UNITY OR A MEF provider..
        //    //  THIS CODE IS FOR THE WCF PROJECT WCF TESTING CLIENT
        //    //  In Production I would anticipate a IIS Service calling this and it woudl use UNITY to DI... 
        //    IDataContextAsync qvContext = new Qv21Context(false);
        //    _unitOfWork = new UnitOfWork(qvContext);

        //    //Arrange
        //    IRepositoryAsync<Site> siteRepository = new Repository<Site>(qvContext);

        //    _siteService = new SiteService(siteRepository);
             
        //}

        public QvSiteService()
        {

        }

        public QvSiteService(ISiteService siteService)
        {
            _siteService = siteService;
        }


        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void CreateSite(Site site)
        {
            _siteService.Insert(site);
            //  _unitOfWork.SaveChanges();

        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public Site GetSite(int siteId)
        {
            //using (IDataContextAsync context = new Qv21Context())
            //using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            //{
            //    return unitOfWork.RepositoryAsync<Site>().Find(siteId);
            //}
            return _siteService.Find(siteId);
        }

        [WebGet]
        public List<Site> GetSites()
        {
            var asyncResult = _siteService.Query().Include(d => d.SiteDetails).SelectAsync();

            return asyncResult.Result.ToList();
        }

        //[WebGet]
        //public List<Site> GetSitesViaService()
        //{
        //    return _siteService.Query().Include(x => x.SiteDetails).SelectAsync().Result.ToList();

        //}

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void UpdateSite(Site site)
        {
            _siteService.Update(site);
            //_unitOfWork.SaveChanges();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void DeleteSite(int siteId)
        {
            _siteService.Delete(siteId);
            //  _unitOfWork.SaveChanges();
        }


        public void Dispose()
        {



        }

    }


}