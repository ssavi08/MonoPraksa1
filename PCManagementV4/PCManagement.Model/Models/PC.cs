namespace PCManagement.WebAPI.Models
{
    public class PC
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string CPU {get; set; }
        public required string GPU {get; set; }
    }
}
