namespace Domain.Models;

public class Client
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}