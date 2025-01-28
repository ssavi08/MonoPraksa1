using PCManagement.Common;
using PCManagement.WebAPI.Models;

namespace PCManagement.RepositoryCommon
{
    public interface IPCRepository
    {
        public bool TestDatabaseConnectionAsync();

        public Task<PC?> GetPCAsync(Guid id);

        public Task<List<PC?>> GetAllPCsAsync(Sorting sorting, PCFilter filter);

        public Task<bool> DeletePCAsync(Guid id);

        public Task<bool> UpdatePCAsync(Guid id, PC updatedPC);

        public Task<bool> AddPCAsync(PC newPc);
    }
}