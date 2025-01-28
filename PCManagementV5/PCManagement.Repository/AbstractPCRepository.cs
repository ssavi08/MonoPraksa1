using PCManagement.Common;
using PCManagement.RepositoryCommon;
using PCManagement.WebAPI.Models;

namespace PCManagement.Repository
{
    public abstract class AbstractPCRepository : IPCRepository
    {
        protected readonly string TableName;
        protected readonly string connText = DatabaseConfig.connString;

        protected AbstractPCRepository(string connString, string tableName)
        {
            connText = connString;
            TableName = tableName;
        }

        public abstract bool TestDatabaseConnectionAsync();

        public abstract Task<PC?> GetPCAsync(Guid id);

        public abstract Task<List<PC?>> GetAllPCsAsync(Sorting sorting, PCFilter filter);

        public abstract Task<bool> DeletePCAsync(Guid id);

        public abstract Task<bool> UpdatePCAsync(Guid id, PC updatedPC);

        public abstract Task<bool> AddPCAsync(PC newPc);
    }
}