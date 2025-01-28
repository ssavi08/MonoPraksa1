using PCManagement.Common;
using PCManagement.WebAPI.Models;

namespace PCManagement.ServiceCommon
{
    public interface IPCService
    {
        bool TestDatabaseConnectionAsync();

        Task<PC?> GetPCAsync(Guid id);

        Task<List<PC?>> GetAllPCsAsync(Sorting sorting);

        Task<bool> DeletePCAsync(Guid id);

        Task<bool> UpdatePCAsync(Guid id, PC updatedPC);

        Task<bool> AddPCAsync(PC newPc);
    }
}