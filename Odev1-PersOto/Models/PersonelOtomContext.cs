using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Odev1_PersOto.Models
{
    public partial class PersonelOtomContext : DbContext
    {
        internal readonly object PersonelBilgi;

        public PersonelOtomContext()
        {
        }

        public PersonelOtomContext(DbContextOptions<PersonelOtomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PersonelBilgi> PersonelBilgis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2QLMN3P\\CEYLAN;Database=PersonelOtom;User ID=SA;Password=1234567; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonelBilgi>(entity =>
            {
                entity.HasKey(e => e.PersonelId)
                    .HasName("PK__Personel__0F0C575167B44CFC");

                entity.ToTable("PersonelBilgi");

                entity.Property(e => e.PersonelId).HasColumnName("PersonelID");

                entity.Property(e => e.Adi).HasMaxLength(20);

                entity.Property(e => e.Adres).HasMaxLength(150);

                entity.Property(e => e.DogumTarihi).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IseGirisTarihi).HasColumnType("datetime");

                entity.Property(e => e.PersonelResmi).HasColumnType("image");

                entity.Property(e => e.Soyadi).HasMaxLength(20);

                entity.Property(e => e.Tckn)
                    .HasMaxLength(12)
                    .HasColumnName("TCKN");

                entity.Property(e => e.TelefonNumarasi).HasMaxLength(20);

                entity.Property(e => e.Unvan).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
