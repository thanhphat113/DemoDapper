using System;
using System.Collections.Generic;

namespace PPDD.Models;

public partial class HoaDonXuat
{
    public int HoaDonXuatId { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual ICollection<ChiTietDonXuat>? ChiTietDonXuats { get; set; } = new List<ChiTietDonXuat>();
}
