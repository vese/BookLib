using System;
using System.Collections.Generic;
using System.Text;
using BookLib.Models.DBModels;

namespace BookLib.Data
{
    public class ApplicationDbContext
    {
        public ApplicationDbContext() { }
        
        public virtual ISet<Author> Author { get; set; }
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
        public virtual DbSet<SheduledBook> SheduledBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.HasOne(d => d.IdBookNavigation).WithOne(p => p.Availability).HasForeignKey<Availability>(d => d.IdBook);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Isbn).IsRequired().HasColumnName("ISBN").HasMaxLength(20);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Books).HasForeignKey(d => d.IdAuthor);

                entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Books).HasForeignKey(d => d.IdCategory);

                entity.HasOne(d => d.IdGenreNavigation).WithMany(p => p.Books).HasForeignKey(d => d.IdGenre);

                entity.HasOne(d => d.IdPublisherNavigation).WithMany(p => p.Books).HasForeignKey(d => d.IdPublisher);

                entity.HasOne(d => d.IdSeriesNavigation).WithMany(p => p.Books).HasForeignKey(d => d.IdSeries);
            });

            modelBuilder.Entity<BookOnHands>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.BooksOnHands).HasForeignKey(d => d.IdUser);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.TakingDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.BooksOnHands).HasForeignKey(d => d.IdBook);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments).HasForeignKey(d => d.IdUser);

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Comments).HasForeignKey(d => d.IdBook);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Genres).HasForeignKey(d => d.IdCategory);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<QueueOnBook>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.QueuesOnBook).HasForeignKey(d => d.IdUser);

                entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.QueuesOnBook).HasForeignKey(d => d.IdBook);
            });

            modelBuilder.Entity<ReadBook>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ReadBooks).HasForeignKey(d => d.IdUser);

                entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.ReadBooks).HasForeignKey(d => d.IdBook);
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SheduledBook>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.SheduledBooks).HasForeignKey(d => d.IdUser);

                entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.SheduledBooks).HasForeignKey(d => d.IdBook);
            });
        }
    }
}
