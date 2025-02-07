namespace PCManagement.WebAPI.Models
{
    public class PC
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CpuModelName { get; set; }
        public string GpuModelName { get; set; }
    }
}