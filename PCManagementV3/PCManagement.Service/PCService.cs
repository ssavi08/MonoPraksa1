using System.Security.Cryptography.X509Certificates;
using PCManagement.Repository;
using PCManagement.WebAPI.Models;

namespace PCManagement.Service
{
    public class PCService
    {
        public async Task<bool> AddPCAsync(PC newPc)
        {
            PCRepository pcRepository = new PCRepository();
            return await pcRepository.AddPCAsync(newPc); 
        }

        public async Task<bool> UpdatePCAsync(Guid id, PC updatedPC)
        {
            PCRepository pcRepository = new PCRepository();
            return await pcRepository.UpdatePCAsync(id, updatedPC);
        }

        public async Task<bool> DeletePCAsync(Guid id)
        {
            PCRepository pcRepository = new PCRepository();

            return await pcRepository.DeletePCAsync(id);
        }

        public async Task<List<PC>> GetAllPCsAsync()
        {
            PCRepository pcRepository = new PCRepository();
            return await pcRepository.GetAllPCsAsync();
        }

        public async Task<PC?> GetPCAsync(Guid id)
        {
            PCRepository pcRepository = new PCRepository();
            return await pcRepository.GetPCAsync(id);
        }

        public async Task<bool> TestDatabaseConnectionAsync()
        {
            PCRepository pcRepository = new PCRepository();
            return await pcRepository.TestDatabaseConnectionAsync();
        }
    }
}
