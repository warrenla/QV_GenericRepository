using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;
using QV.Service.Contracts;
using QV.WcfServiceLibrary.Contracts;

namespace QV.WcfServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class QvSiteDetailService : IWCFQvSiteDetailService
    {
        private readonly ISiteDetailServce _service;

        public QvSiteDetailService()
        {

        }

        public QvSiteDetailService(ISiteDetailServce service)
        {
            _service = service;
        }

        [WebInvoke]
        public SiteDetail Get(int Id)
        {
            return _service.Find(Id);
        }

        [WebGet]
        public List<SiteDetail> GetList()
        {
            var asyncResult = _service.Query().SelectAsync();
            return asyncResult.Result.ToList();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Create(SiteDetail siteDetail)
        {
            _service.Insert(siteDetail);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Update(SiteDetail siteDetail)
        {
            _service.Update(siteDetail);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke(Method = "DELETE")]
        public void Delete(int Id)
        {
            _service.Delete(Id);
        }

        public void Dispose()
        {

        }
    }
}