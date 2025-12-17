
namespace Domain.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
