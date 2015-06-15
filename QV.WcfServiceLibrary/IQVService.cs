using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using QV.Data.Models;
using Repository.Pattern.Infrastructure;

namespace QV.WcfServiceLibrary
{
    [ServiceContract]
    public interface IWCFQvSiteService : IDisposable
    {
        [OperationContract()]
        [WebGet]
        Site GetSite(int siteId);
        
        [OperationContract()]
        [WebGet]
        List<Site> GetSites();

        //[OperationContract]
        //[WebGet]
        //List<Site> GetSitesViaService();

        [OperationContract]
        [WebInvoke]
        void CreateSite(Site site);

        [OperationContract]
        [WebInvoke]
        void UpdateSite(Site site);

        [OperationContract]
        [WebInvoke]
        void DeleteSite(int siteId);


       
    }

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