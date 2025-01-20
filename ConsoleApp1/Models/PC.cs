using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCManagement.Models
{
    public abstract class PC
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public  string? CPU { get; set; }
        public  string? GPU { get; set; }
        public int RAMSize { get; set; }
        public int Storage { get; set; }
        public string? UseCase { get; set; }

        public abstract string GetPCType();

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Type: {GetPCType()}, Use case: {UseCase}, CPU: {CPU}, GPU: {GPU}, RAM size: {RAMSize}, Storage: {Storage}";
        }
    }
}
