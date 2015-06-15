using QV.Data.Models;
using QV.Service.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace QV.Service
{
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