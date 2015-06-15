using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;

namespace QV.WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IWCFQvSiteService : IDisposable
    {
        [OperationContract()]
        [WebGet]
        Site Get(int siteId);
        
        [OperationContract()]
        [WebGet]
        List<Site> GetList();

        //[OperationContract]
        //[WebGet]
        //List<Site> GetSitesViaService();

        [OperationContract]
        [WebInvoke]
        void Create(Site site);

        [OperationContract]
        [WebInvoke]
        void Update(Site site);

        [OperationContract]
        [WebInvoke]
        void Delete(int siteId);


       
    }
}