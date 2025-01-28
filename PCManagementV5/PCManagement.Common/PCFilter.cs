using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCManagement.Common
{
    public class PCFilter
    {
        public string? SearchQuery { get; set; }
        public string? CPU {  get; set; }
        public string? GPU { get; set; }
    }
}