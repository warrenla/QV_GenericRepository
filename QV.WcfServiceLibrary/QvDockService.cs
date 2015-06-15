using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Threading;
using System.Threading.Tasks;
using QV.Data.Models;
using QV.Service;
using QV.Service.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace QV.WcfServiceLibrary
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class QvDockService : IWCFQvDockService
    {
        private readonly IDockService _service;

        public QvDockService()
        {

        }

        public QvDockService(IDockService service)
        {
            _service = service;
        }

        [WebInvoke]
        public Dock Get(int Id)
        {
            return _service.Find(Id);
        }

        [WebInvoke]
        public List<Dock> GetList()
        {
            var asyncResult = _service.Query().SelectAsync();
            return asyncResult.Result.ToList();

        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Create(Dock dock)
        {
            _service.Insert(dock);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [WebInvoke]
        public void Update(Dock dock)
        {
            _service.Update(dock);
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