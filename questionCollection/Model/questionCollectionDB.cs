using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace questionCollection.Model
{
    public partial class questionCollectionDB : DbContext
    {
        public questionCollectionDB()
        {
        }

        public questionCollectionDB(DbContextOptions<questionCollectionDB> options)
            : base(options)
        {
        }

        public virtual DbSet<Questions> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.Property(e => e.Author).IsUnicode(false);

                entity.Property(e => e.ClassName).IsUnicode(false);

                entity.Property(e => e.ClassNumber).IsUnicode(false);

                entity.Property(e => e.Institution).IsUnicode(false);

                entity.Property(e => e.QuestionType).IsUnicode(false);
            });
        }
    }
}
