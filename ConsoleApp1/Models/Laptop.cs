using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCManagement.Models
{
    public class Laptop : PC
    {
        public override string GetPCType() => "Laptop";
    }
}
