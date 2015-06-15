using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;

namespace QV.WcfServiceLibrary.Contracts
{
    [ServiceContract]
    public interface IWCFQvSiteDetailService : IDisposable
    {
        [OperationContract()]
        [WebGet]
        SiteDetail Get(int Id);

        [OperationContract()]
        [WebGet]
        List<SiteDetail> GetList();

        [OperationContract]
        [WebInvoke]
        void Create(SiteDetail siteDetail);

        [OperationContract]
        [WebInvoke]
        void Update(SiteDetail siteDetail);

        [OperationContract]
        [WebInvoke]
        void Delete(int Id);



    }
}