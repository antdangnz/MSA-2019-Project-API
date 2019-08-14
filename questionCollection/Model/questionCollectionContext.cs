using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuestionCollection.Model
{
    public partial class questionCollectionContext : DbContext
    {
        public questionCollectionContext()
        {
        }

        public questionCollectionContext(DbContextOptions<questionCollectionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:questioncollect.database.windows.net,1433;Initial Catalog=questionCollection;Persist Security Info=False;User ID=adan849;Password=ms@li0nheart;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Question__6238D4B25088066B");

                entity.Property(e => e.Author).IsUnicode(false);

                entity.Property(e => e.ClassName).IsUnicode(false);

                entity.Property(e => e.ClassNumber).IsUnicode(false);

                entity.Property(e => e.Institution).IsUnicode(false);

                entity.Property(e => e.QuestionType).IsUnicode(false);
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.HasKey(e => e.RatingId)
                    .HasName("PK__Ratings__2D290CA94C77A440");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("questionId");
            });
        }
    }
}
