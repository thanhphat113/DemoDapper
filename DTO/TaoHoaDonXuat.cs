using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPDD.Models;

namespace PPDD.DTO
{
	public class TaoHoaDonXuat
	{
		public DateTime NgayTao { get; set; }

		public ICollection<SanPhamXuat> SanPhams { get; set; } = new List<SanPhamXuat>();
	}
}