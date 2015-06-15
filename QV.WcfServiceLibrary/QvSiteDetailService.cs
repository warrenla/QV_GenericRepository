using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using QV.Data.Models;
using QV.Service;

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


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public SiteDetail Get(int Id)
        {
            return _service.Find(Id);
        }

        public List<SiteDetail> GetList()
        {
            var asyncResult = _service.Query().SelectAsync();
            return asyncResult.Result.ToList();
        }

        public void Create(SiteDetail siteDetail)
        {
            _service.Insert(siteDetail);
        }

        public void Update(SiteDetail siteDetail)
        {
            _service.Update(siteDetail);
        }

        public void Delete(int Id)
        {
            _service.Delete(Id);
        }
    }
}