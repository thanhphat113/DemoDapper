using System;
using System.Collections.Generic;

namespace PPDD.Models;

public partial class HoaDonNhap
{
    public int HoaDonNhapId { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual ICollection<LoHang>? LoHangs { get; set; } = new List<LoHang>();
}
