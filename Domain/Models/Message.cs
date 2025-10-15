using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Message
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public string Author { get; set; } = null!;

    public string Text { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Chat Chat { get; set; } = null!;
}
