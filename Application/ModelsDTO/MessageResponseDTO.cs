using Domain.Models;

namespace Application.ModelsDTO;

public class MessageResponseDTO
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public string Author { get; set; } = null!;

    public string Text { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    
}
