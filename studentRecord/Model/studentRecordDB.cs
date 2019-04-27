using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace studentRecord.Model
{
    public partial class studentRecordDB : DbContext
    {
        public studentRecordDB()
        {
        }

        public studentRecordDB(DbContextOptions<studentRecordDB> options)
            : base(options)
        {
        }

        public virtual DbSet<Students> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.EmergencyContact).IsUnicode(false);

                entity.Property(e => e.EmergencyEmail).IsUnicode(false);

                entity.Property(e => e.EmergencyNumber).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.Homeroom).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.Year).IsUnicode(false);
            });
        }
    }
}
