using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users {get; set; }
        public DbSet<UserLike> Likes {get; set; }
        public DbSet<Message> Messages {get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>()
                .HasKey(key => new {key.SourceUserId, key.LikedUserId} );
            
            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(f => f.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);          //for SQL Server, this shd be DeleveBehavior.NoAction
            
            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(f => f.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);          //for SQL Server, this shd be DeleveBehavior.NoAction
            
            builder.Entity<Message>()
                .HasOne(s => s.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Message>()
                .HasOne(s => s.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}