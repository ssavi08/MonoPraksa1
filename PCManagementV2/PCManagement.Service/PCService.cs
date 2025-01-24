using System.Security.Cryptography.X509Certificates;
using PCManagement.Repository;
using PCManagement.WebAPI.Models;

namespace PCManagement.Service
{
    public class PCService
    {
        public bool AddPc(PC newPc)
        {
            PCRepository pcRepository = new PCRepository();

            bool result = pcRepository.AddPC(newPc);
            return result;
        }

        public bool UpdatePC(Guid id, PC updatedPC)
        {
            PCRepository pcRepository = new PCRepository();

            bool result = pcRepository.UpdatePC(id, updatedPC);
            return result;
        }

        public bool DeletePC(Guid id)
        {
            PCRepository pcRepository = new PCRepository();

            bool result = pcRepository.DeletePC(id);
            return result;
        }

        public List<PC> GetAll()
        {
            PCRepository pcRepository = new PCRepository();
            List<PC> result = pcRepository.GetAll();
            return result;
        }

        public PC? GetPc(Guid id)
        {
            PCRepository pcRepository = new PCRepository();
            return pcRepository.GetPC(id);
        }

        public bool TestConnection()
        {
            PCRepository pcRepository = new PCRepository();
            return pcRepository.TestDatabaseConnection();
        }
    }
}
