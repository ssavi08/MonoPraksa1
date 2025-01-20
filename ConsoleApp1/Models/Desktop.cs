using PCManagement.Models;

namespace PCManagementApp.Models
{
    public class Desktop : PC
    {
        public override string GetPCType() => "Desktop";
    }
}
