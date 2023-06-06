using BookEvent.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BookEvent.DataAccessLayer
{
    public class BookEventDBContext : IdentityDbContext<ApplicationUser>
    {
        public BookEventDBContext(DbContextOptions<BookEventDBContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Invitations)
                .WithOne(i => i.ApplicationUser)
                .HasForeignKey(i => i.ApplicationUserId);

            builder.Entity<Event>()
                .HasMany(e => e.Invitations)
                .WithOne(i => i.Event)
                .HasForeignKey(i => i.EventId);

            builder.Entity<Event>()
                .HasMany(e => e.Comments)
                .WithOne(c => c.Event)
                .HasForeignKey(c => c.EventId);
        }
    }
}
