using QV.Data.Models;
using QV.Service.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace QV.Service
{
    public class DockService : Service<Dock>, IDockService
    {
        private readonly IRepositoryAsync<Dock> _repository;

        public DockService(IRepositoryAsync<Dock> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}