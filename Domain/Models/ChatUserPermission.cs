
namespace Domain.Models
{
    public partial class ChatUserPermission
    {
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public Permission Permission { get; set; } = null!;
    }
}
