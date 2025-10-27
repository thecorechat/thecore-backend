using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.DB;

public partial class SchoolChatContext : DbContext
{
    public SchoolChatContext()
    {
    }

    public SchoolChatContext(DbContextOptions<SchoolChatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Chat> Chats { get; set; }
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
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id).HasName("PK_Users_Id");
            entity.Property(u => u.Id).ValueGeneratedOnAdd();

            entity.HasIndex(u => u.Identifier).IsUnique();
            entity.Property(u => u.Identifier).IsRequired().HasDefaultValueSql("(newId())");

            entity.Property(u => u.Username).IsRequired().HasMaxLength(256);

            entity.Property(u => u.IconUrl).IsRequired(false);


        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name).IsRequired().HasMaxLength(256);

            entity.Property(e => e.Description).IsRequired(false).HasMaxLength(512);
        });

        modelBuilder.Entity<Role>().HasData(
            new Role() { Name = "Admin", Description = "Have permissions for everything" },
            new Role() { Name = "Teacher", Description = "Can read, write(restricted), delete and change(only materials that owns to him or himsels class)" },
            new Role() { Name = "Student", Description = "Can read(publicly available or himself class material), write(restricted), delete and change(only material that it owns)" },
            new Role() { Name = "Guest", Description = "Car read(publicly available or materials attached to him)" }
        );

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chats__3214EC07BC974D61");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("chats");

            

            entity.Property(e => e.Title).HasMaxLength(256);
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
