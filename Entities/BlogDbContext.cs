using Microsoft.EntityFrameworkCore;

namespace DevBlog.Entities
{
    public class BlogDbContext : DbContext
    {

        public DbSet<User> User { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Reaction> Reaction { get; set; }

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filter
            modelBuilder.Entity<User>().HasQueryFilter(u => u.DeletedAt == null);

            modelBuilder.Entity<User>()
                .HasIndex(t => t.Email).IsUnique();

            modelBuilder.Entity<Follow>()
                .HasKey(pf => new { pf.UserId, pf.FollowerId });
            
            modelBuilder.Entity<Follow>()
                .HasOne(pf => pf.User)
                .WithMany(p => p.Followers)
                .HasForeignKey(pf => pf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(pf => pf.Follower)
                .WithMany()
                .HasForeignKey(pf => pf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Posts)
                .WithMany(p => p.Tags)
                .UsingEntity(j => j.ToTable("PostTag"));

             modelBuilder.Entity<Post>()
                .HasOne(p => p.category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
