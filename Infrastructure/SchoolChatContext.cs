using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Infrastructure;

public partial class SchoolChatContext : DbContext
{
    public SchoolChatContext()
    {
    }

    public SchoolChatContext(DbContextOptions<SchoolChatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Message> Messages { get; set; }

    private string GetConnectionString(string connectionStringConfigurationKey = "ConnectionStrings:DefaultConnection")
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<SchoolChatContext>()
            .Build();
        return configuration[connectionStringConfigurationKey] ?? throw new Exception("Connection string does not configured");
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chats__3214EC07BC974D61");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("chats");

            entity.HasMany(e => e.Clients)
                .WithMany(c => c.Chats)
                .UsingEntity<Dictionary<string, object>>(
                    "ChatClient",
                    j => j.HasOne<Client>().WithMany().OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Chat>().WithMany().OnDelete(DeleteBehavior.Cascade)
                );

            entity.Property(e => e.Title).HasMaxLength(256);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Clients_Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasMany(e => e.Chats)
                .WithMany(c => c.Clients);

        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3214EC07CF1AB666");

            entity.ToTable("messages");

            entity.Property(e => e.Author).HasMaxLength(256);
            entity.Property(e => e.ChatId).HasColumnName("Chat_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.Text).HasColumnName("Text");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Char_Id_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
