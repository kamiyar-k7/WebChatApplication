

using Doamin.Entities.ChatEntites;
using Doamin.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace InfruStructure.WebChatDbContext;

public class ChatDbContext : DbContext
{

    #region Ctor

    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {

    }
    #endregion

    //users
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserSelectedRole> UserSelectedRoles { get; set; }


    //  chats
    public DbSet<Messages> Messages { get; set; }
    public DbSet<Conversation> conversations { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.Property(u => u.UserName).HasMaxLength(30);
            user.Property(u => u.FirstName).HasMaxLength(30);
            user.Property(u => u.LastName).HasMaxLength(30);
            
    
        });

        modelBuilder.Entity<Messages>(message =>
        {
            message.Property(m => m.Content).HasMaxLength(1000);

            // Configure Sender relationship
            message.HasOne(m => m.Sender)
                .WithMany()  // No navigation property in User for messages
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Configure Receiver relationship
            message.HasOne(m => m.Resiver)
                .WithMany()  // No navigation property in User for messages
                .HasForeignKey(m => m.ResiverId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            message.HasOne(m=> m.Conversation).WithMany(m=> m.messages).HasForeignKey(m=> m.ConversationId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Conversation>(coverstation =>
        {


            // Configure Sender relationship
            coverstation.HasOne(m => m.User1)
                .WithMany()  // No navigation property in User for messages
                .HasForeignKey(m => m.User1Id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Configure Sender relationship
            coverstation.HasOne(m => m.User2)
                .WithMany()  // No navigation property in User for messages
                .HasForeignKey(m => m.User2Id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        });

        base.OnModelCreating(modelBuilder);
    }
}
