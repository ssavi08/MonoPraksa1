namespace PCManagement.WebAPI.Models
{
    public class PC
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? CPU { get; set; }
        public string? GPU { get; set; }
    }
}