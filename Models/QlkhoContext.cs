using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PPDD.Models;

public partial class QlkhoContext : DbContext
{
    public QlkhoContext()
    {
    }

    public QlkhoContext(DbContextOptions<QlkhoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonXuat> ChiTietDonXuats { get; set; }

    public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }

    public virtual DbSet<HoaDonXuat> HoaDonXuats { get; set; }

    public virtual DbSet<LoHang> LoHangs { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=QLKho;User Id=sa;Password=123456aA@$;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonXuat>(entity =>
        {
            entity.HasKey(e => e.ChiTietXuatId).HasName("PK__ChiTietD__B2A28E432E55F305");

            entity.ToTable("ChiTietDonXuat");

            entity.HasIndex(e => e.ChiTietXuatId, "UQ__ChiTietD__B2A28E42A026EB0A").IsUnique();

            entity.Property(e => e.ChiTietXuatId).HasColumnName("ChiTietXuatID");
            entity.Property(e => e.HoaDonXuatId).HasColumnName("HoaDonXuatID");
            entity.Property(e => e.LoHangId).HasColumnName("LoHangID");

            entity.HasOne(d => d.HoaDonXuat).WithMany(p => p.ChiTietDonXuats)
                .HasForeignKey(d => d.HoaDonXuatId)
                .HasConstraintName("FK__ChiTietDo__HoaDo__44FF419A");

            entity.HasOne(d => d.LoHang).WithMany(p => p.ChiTietDonXuats)
                .HasForeignKey(d => d.LoHangId)
                .HasConstraintName("FK__ChiTietDo__LoHan__45F365D3");
        });

        modelBuilder.Entity<HoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.HoaDonNhapId).HasName("PK__HoaDonNh__47DF860289DA9C63");

            entity.ToTable("HoaDonNhap");

            entity.HasIndex(e => e.HoaDonNhapId, "UQ__HoaDonNh__47DF860389AE06D8").IsUnique();

            entity.Property(e => e.HoaDonNhapId).HasColumnName("HoaDonNhapID");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        modelBuilder.Entity<HoaDonXuat>(entity =>
        {
            entity.HasKey(e => e.HoaDonXuatId).HasName("PK__HoaDonXu__D8438AF8D43DA5B7");

            entity.ToTable("HoaDonXuat");

            entity.HasIndex(e => e.HoaDonXuatId, "UQ__HoaDonXu__D8438AF934E8B522").IsUnique();

            entity.Property(e => e.HoaDonXuatId).HasColumnName("HoaDonXuatID");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        modelBuilder.Entity<LoHang>(entity =>
        {
            entity.HasKey(e => e.LoHangId).HasName("PK__LoHang__AAA88484B1237E2A");

            entity.ToTable("LoHang");

            entity.HasIndex(e => e.LoHangId, "UQ__LoHang__AAA8848551BD74D2").IsUnique();

            entity.Property(e => e.LoHangId).HasColumnName("LoHangID");
            entity.Property(e => e.HoaDonNhapId).HasColumnName("HoaDonNhapID");
            entity.Property(e => e.SanPhamId).HasColumnName("SanPhamID");

            entity.HasOne(d => d.HoaDonNhap).WithMany(p => p.LoHangs)
                .HasForeignKey(d => d.HoaDonNhapId)
                .HasConstraintName("FK__LoHang__HoaDonNh__440B1D61");

            entity.HasOne(d => d.SanPham).WithMany(p => p.LoHangs)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoHang__SanPhamI__46E78A0C");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.SanPhamId).HasName("PK__SanPham__05180FF4448C0F04");

            entity.ToTable("SanPham");

            entity.HasIndex(e => e.SanPhamId, "UQ__SanPham__05180FF5C4DF357B").IsUnique();

            entity.Property(e => e.SanPhamId).HasColumnName("SanPhamID");
            entity.Property(e => e.TenSp)
                .HasMaxLength(255)
                .HasColumnName("TenSP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
