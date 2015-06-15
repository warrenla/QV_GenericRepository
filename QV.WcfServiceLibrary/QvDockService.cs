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


        //[OperationBehavior(TransactionScopeRequired = true)]
        //[WebInvoke]
        //public void CreateDock(Dock dock)
        //{
        //    _repository.Insert(dock);

        //}

        //[OperationBehavior(TransactionScopeRequired = true)]
        //[WebInvoke]
        //public Dock GetDock(int dockId)
        //{

        //    return _repository.Find(dockId);
        //}

    

        public void Dispose()
        {



        }

        public Dock Get(int Id)
        {
            return _service.Find(Id);
        }

        public List<Dock> GetList()
        {
            var asyncResult = _service.Query().SelectAsync();
            return asyncResult.Result.ToList();

        }

        public void Create(Dock dock)
        {
           _service.Insert(dock);
        }

        public void Update(Dock dock)
        {
            _service.Update(dock);
        }

        public void Delete(int Id)
        {
          _service.Delete(Id);
        }
    }
}