using System.Security.Cryptography.X509Certificates;
using Autofac.Core;
using PCManagement.Repository;
using PCManagement.RepositoryCommon;
using PCManagement.WebAPI.Models;

namespace PCManagement.Service
{
    public class PCService : AbstractPCService
    {
        private readonly IPCRepository _repository;

        public PCService(IPCRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }

    //public async Task<bool> AddPCAsync(PC newPc)
    //{
    //    //PCRepository pcRepository = new PCRepository();
    //    return await _repository.AddPCAsync(newPc);
    //}

    //public async   bool UpdatePCAsync(Guid id, PC updatedPC)
    //{
    //    return await _repository.UpdatePCAsync(id, updatedPC);
    //}

    //public async Task<bool> DeletePCAsync(Guid id)
    //{
    //    return await _repository.DeletePCAsync(id);
    //}

    //public async Task<List<PC>> GetAllPCsAsync()
    //{
    //    return await _repository.GetAllPCsAsync();
    //}

    //public async Task<PC?> GetPCAsync(Guid id)
    //{
    //    return await _repository.GetPCAsync(id);
    //}

    //public async Task<bool> TestDatabaseConnectionAsync()
    //{
    //    return await _repository.TestDatabaseConnectionAsync();
    //}
}