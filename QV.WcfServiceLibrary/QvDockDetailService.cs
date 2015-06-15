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
    public class QvDockDetailService : IWCFQvDockDetailService
    {
        private readonly IDockDetailServce _service;

        public QvDockDetailService()
        {

        }

        public QvDockDetailService(IDockDetailServce service)
        {
            _service = service;
        }

        [WebInvoke]
        public DockDetail Get(int Id)
        {
            return _service.Find(Id);
        }

        [WebGet]
        public List<DockDetail> GetList()
        {
            var asyncResult = _service.Query().SelectAsync();
            return asyncResult.Result.ToList();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Create(DockDetail DockDetail)
        {
            _service.Insert(DockDetail);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Update(DockDetail DockDetail)
        {
            _service.Update(DockDetail);
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