using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using UserPostApi.Models.Entities;

namespace UserPostApi.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Comment>()
                        .HasOne(C=>C.User)
                        .WithMany(U=>U.Comments)
                        .HasForeignKey(U=>U.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                        .HasOne(C=>C.Post)
                        .WithMany(P=>P.Comments)
                        .HasForeignKey(C=>C.PostId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
            
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
