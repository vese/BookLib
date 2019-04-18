using BookLib.Models.DBModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookLib.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Availability> Availability { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookOnHands> BookOnHands { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<QueueOnBook> QueueOnBook { get; set; }
        public virtual DbSet<ReadBook> ReadBook { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<ScheduledBook> ScheduledBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.HasOne(d => d.BookNavigation).WithOne(p => p.Availability).HasForeignKey<Availability>(d => d.BookId);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books).HasForeignKey(d => d.AuthorId);
                entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Books).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Books).HasForeignKey(d => d.GenreId);
                entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Books).HasForeignKey(d => d.PublisherId);
                entity.HasOne(d => d.SeriesNavigation).WithMany(p => p.Books).HasForeignKey(d => d.SeriesId);
            });

            modelBuilder.Entity<BookOnHands>(entity =>
            {
                entity.HasOne(d => d.UserNavigation).WithMany(p => p.BooksOnHands).HasForeignKey(d => d.UserId);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.TakingDate).HasColumnType("datetime");

                entity.HasOne(d => d.BookNavigation).WithMany(p => p.BooksOnHands).HasForeignKey(d => d.BookId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.UserNavigation).WithMany(p => p.Comments).HasForeignKey(d => d.UserId);

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.BookNavigation).WithMany(p => p.Comments).HasForeignKey(d => d.BookId);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Genres).HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<QueueOnBook>(entity =>
            {
                entity.HasOne(d => d.UserNavigation).WithMany(p => p.QueuesOnBook).HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.BookNavigation).WithMany(p => p.QueuesOnBook).HasForeignKey(d => d.BookId);
            });

            modelBuilder.Entity<ReadBook>(entity =>
            {
                entity.HasOne(d => d.UserNavigation).WithMany(p => p.ReadBooks).HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.BookNavigation).WithMany(p => p.ReadBooks).HasForeignKey(d => d.BookId);
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ScheduledBook>(entity =>
            {
                entity.HasOne(d => d.UserNavigation).WithMany(p => p.ScheduledBooks).HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.BookNavigation).WithMany(p => p.ScheduledBooks).HasForeignKey(d => d.BookId);
            });
        }
    }
}
