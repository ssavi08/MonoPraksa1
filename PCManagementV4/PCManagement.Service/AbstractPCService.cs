using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCManagement.Repository;
using PCManagement.RepositoryCommon;
using PCManagement.ServiceCommon;
using PCManagement.WebAPI.Models;

namespace PCManagement.Service
{
    public abstract class AbstractPCService : IPCService
    {
        private readonly IPCRepository _repository;

        protected AbstractPCService(IPCRepository repository)
        {
            _repository = repository;
        }
        public bool TestDatabaseConnectionAsync()
        {
            return _repository.TestDatabaseConnectionAsync();
        }

        public Task<PC?> GetPCAsync(Guid id)
        {
            return _repository.GetPCAsync(id);
        }

        public async Task<List<PC?>> GetAllPCsAsync()
        {
            return await _repository.GetAllPCsAsync();
        }

        public async Task<bool> DeletePCAsync(Guid id)
        {
            return await _repository.DeletePCAsync(id);
        }

        public async Task<bool> UpdatePCAsync(Guid id, PC updatedPC)
        {
            return await(_repository.UpdatePCAsync(id, updatedPC));
        }

        public Task<bool> AddPCAsync(PC newPc)
        {
            return  _repository.AddPCAsync(newPc);
        }
    }
}
