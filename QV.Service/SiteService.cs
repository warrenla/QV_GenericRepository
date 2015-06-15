using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using QV.Data.Models;
using QV.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace QV.Service
{
    public interface IDockService : IService<Dock>
    {
        
    }

    public class DockService : Service<Dock>, IDockService
    {
        private readonly IRepositoryAsync<Dock> _repository;

        public DockService(IRepositoryAsync<Dock> repository) : base(repository)
        {
            _repository = repository;
        }
    }

    public interface ISiteService : IService<Site>
    {
        //int GetSiteDockCount(int siteId);

        //void CreateSite(Site newSite);
    }

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

    public interface ISiteDetailServce : IService<SiteDetail>
    {

    }

    public class SiteDetailService : Service<SiteDetail>, ISiteDetailServce
    {
        private readonly IRepositoryAsync<SiteDetail> _repository;

        public SiteDetailService(IRepositoryAsync<SiteDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }



}