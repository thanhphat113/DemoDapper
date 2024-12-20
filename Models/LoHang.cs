using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PPDD.Models;

public partial class LoHang
{
    public int LoHangId { get; set; }

    public int SanPhamId { get; set; }

    public double SoLuong { get; set; }

    public double GiaNhap { get; set; }

    public int HoaDonNhapId { get; set; }


    public virtual ICollection<ChiTietDonXuat>? ChiTietDonXuats { get; set; } = new List<ChiTietDonXuat>();


    public virtual HoaDonNhap? HoaDonNhap { get; set; } = null!;

    public virtual SanPham? SanPham { get; set; } = null!;
}
