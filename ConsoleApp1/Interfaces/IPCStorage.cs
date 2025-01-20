using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCManagement.Models;



namespace PCManagement.Interfaces
{
    public interface IPCStorage
    {
        void AddPC(PC pc);
        //void EditPC(int id, Action<PC> editAction);
        void DeletePC(int id);
        IEnumerable<PC> GetAllPCs();
        PC? GetPCById(int id);
    }
}
