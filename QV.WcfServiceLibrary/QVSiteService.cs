using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;
using QV.Service;
using QV.Service.Contracts;
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
        //    //  THIS CODE IS FOR THE WCF PROJECT WCF TESTING CLIENT - do not use. Strictly for quick initial build out and quick test.
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
        public void Create(Site site)
        {
            _siteService.Insert(site);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public Site Get(int siteId)
        {
            return _siteService.Find(siteId);
        }

        [WebGet]
        public List<Site> GetList()
        {
            var asyncResult = _siteService.Query().Include(d => d.SiteDetails).SelectAsync();

            return asyncResult.Result.ToList();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Update(Site site)
        {
            _siteService.Update(site);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Delete(int siteId)
        {
            _siteService.Delete(siteId);
        }


        public void Dispose()
        {



        }

    }


}