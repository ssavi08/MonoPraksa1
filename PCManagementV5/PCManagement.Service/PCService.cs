using PCManagement.RepositoryCommon;

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
}