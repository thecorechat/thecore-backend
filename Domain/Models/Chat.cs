using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Chat
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
