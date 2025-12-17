namespace Domain.Models;

public partial class Message
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public string Author { get; set; } = null!;

    public string? Text { get; set; } = null!;
    // ToDo 
    public List<MessageAttachment?> Files { get; set; } = new List<MessageAttachment?>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }

    public virtual Chat Chat { get; set; } = null!;
}
