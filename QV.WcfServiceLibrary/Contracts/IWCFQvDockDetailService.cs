using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;

namespace QV.WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IWCFQvDockDetailService : IDisposable
    {
        [OperationContract()]
        [WebGet]
        DockDetail Get(int Id);

        [OperationContract()]
        [WebGet]
        List<DockDetail> GetList();

        [OperationContract]
        [WebInvoke]
        void Create(DockDetail dockDetail);

        [OperationContract]
        [WebInvoke]
        void Update(DockDetail dockDetail);

        [OperationContract]
        [WebInvoke]
        void Delete(int Id);



    }
}