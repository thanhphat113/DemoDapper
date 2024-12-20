using System;
using System.Collections.Generic;

namespace PPDD.Models;

public partial class SanPham
{
    public int SanPhamId { get; set; }

    public string TenSp { get; set; } = null!;

    public virtual ICollection<LoHang>? LoHangs { get; set; } = new List<LoHang>();

}
