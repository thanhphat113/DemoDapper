using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PPDD.Models;

public partial class ChiTietDonXuat
{
    public int LoHangId { get; set; }

    public int HoaDonXuatId { get; set; }

    public double SoLuong { get; set; }

    public int ChiTietXuatId { get; set; }

    public virtual HoaDonXuat? HoaDonXuat { get; set; } = null!;

    public virtual LoHang? LoHang { get; set; } = null!;
}
