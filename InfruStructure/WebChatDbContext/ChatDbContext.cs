

using Doamin.Entities.Chats;
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

    // chats
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<UserChatRoom> UserChatRooms { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<User>(user =>
        {
            user.Property(name => name.UserName).HasMaxLength(20);
            user.Property(email => email.UserEmail).HasMaxLength(30);

        });

        modelBuilder.Entity<Message>(message =>
        {
            message.Property(message => message.Content).HasMaxLength(1000);
        });


        base.OnModelCreating(modelBuilder);
    }
}
