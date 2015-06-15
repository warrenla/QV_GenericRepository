using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;

namespace QV.WcfServiceLibrary
{
    [ServiceContract]
    public interface IWCFQvDockService : IDisposable
    {
        [OperationContract()]
        [WebGet]
        Dock Get(int Id);

        [OperationContract()]
        [WebGet]
        List<Dock> GetList();

        [OperationContract]
        [WebInvoke]
        void Create(Dock dock);

        [OperationContract]
        [WebInvoke]
        void Update(Dock dock);

        [OperationContract]
        [WebInvoke]
        void Delete(int Id);



    }
}