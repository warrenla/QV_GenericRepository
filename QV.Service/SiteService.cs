using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using QV.Data.Models;
using QV.Repository.Repositories;
using QV.Service.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace QV.Service
{
    public class SiteService : Service<Site>, ISiteService
    {


        private readonly IRepositoryAsync<Site> _asynRepository;

        public SiteService(IRepositoryAsync<Site> repository)
            : base(repository)
        {
            _asynRepository = repository;
        }


        //public IEnumerable<Site> Get()
        //{
        //    return _asynRepository.Query().Select();
        //}

        //public int GetSiteDockCount(int siteId)
        //{
        //    return _asynRepository.GetSiteCountDemo();

        //}



        //public void CreateSite(Site newSite)
        //{
        //    _asynRepository.Insert(newSite);
        //}


    }
}