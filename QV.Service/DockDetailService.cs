using QV.Data.Models;
using QV.Service.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace QV.Service
{
    public class DockDetailService : Service<DockDetail>, IDockDetailServce
    {
        private readonly IRepositoryAsync<DockDetail> _repository;

        public DockDetailService(IRepositoryAsync<DockDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}