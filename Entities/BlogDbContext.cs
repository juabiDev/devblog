using Microsoft.EntityFrameworkCore;

namespace DevBlog.Entities
{
    public class BlogDbContext : DbContext
    {

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Follow> Follow { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Reaction> Reaction { get; set; }

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                opt => {
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }
             );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filter
            modelBuilder.Entity<User>().HasQueryFilter(u => u.DeletedAt == null);
            modelBuilder.Entity<Post>().HasQueryFilter(p => p.DeletedAt == null);
            modelBuilder.Entity<Comment>().HasQueryFilter(c => c.DeletedAt == null);

            modelBuilder.Entity<Comment>()
                .HasQueryFilter(c => c.Author == null || c.Author.DeletedAt == null);

            modelBuilder.Entity<Follow>()
                .HasQueryFilter(f => f.Follower == null || f.Follower.DeletedAt == null);

            modelBuilder.Entity<User>
                (entity =>
                {
                    entity.HasIndex(e => e.UserName).IsUnique();
                    entity.HasIndex(e => e.Email).IsUnique();
                });

            modelBuilder.Entity<Follow>()
                .HasKey(pf => new { pf.FollowedId, pf.FollowerId });
            
            modelBuilder.Entity<Follow>()
                .HasOne(pf => pf.Followed)
                .WithMany(p => p.Followers)
                .HasForeignKey(pf => pf.FollowedId)
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
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
