using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCManagement.Models;
using PCManagement.Interfaces;

namespace PCManagement.Repositories
{
    public class PCStorage : IPCStorage
    {
        private List<PC> pcs = new();
        private int nextId = 1;

        public void AddPC(PC pc)
        {
            pc.Id = nextId++;
            pcs.Add(pc);
        }

       /* public void EditPC(int id, Action<PC> editAction)
        {
            var pc = GetPCById(id);
            if (pc != null) editAction(pc);
        }*/
        public void DeletePC(int id)
        {
            var pc = GetPCById(id);
            if (pc != null) pcs.Remove(pc);
        }

        public IEnumerable<PC> GetAllPCs() => pcs;
        public PC? GetPCById(int id) => pcs.FirstOrDefault(pc => pc.Id == id);
    }
}
