using Microsoft.EntityFrameworkCore;
using PPDD.DTO;
using PPDD.Models;

namespace PPDD.Services
{
	public interface IHoaDonXuatService
	{
		Task<dynamic> AddAsync(TaoHoaDonXuat invoice);
		Task<dynamic> FindAll();
		Task<dynamic> Delete(int Id);
	}

	public class HoaDonXuatService : IHoaDonXuatService
	{
		private readonly QlkhoContext _context;

		public HoaDonXuatService(QlkhoContext context)
		{
			_context = context;
		}

		public async Task<dynamic> AddAsync(TaoHoaDonXuat invoice)
		{
			if (invoice.SanPhams == null || !invoice.SanPhams.Any())
			{
				return new { IsSuccess = false, Message = "Không thể tạo một hoá đơn rỗng" };
			}

			var hdx = new HoaDonXuat
			{
				NgayTao = invoice.NgayTao,
			};

			var SanPhams = await _context.SanPhams.Include(s => s.LoHangs).ToListAsync();

			var ChiTiets = await _context.ChiTietDonXuats.Include(c => c.LoHang).ToListAsync();

			foreach (var item in invoice.SanPhams)
			{
				var sp = SanPhams.FirstOrDefault(h => h.SanPhamId == item.SanPhamId);

				var chitiet = ChiTiets.Where(c => c.LoHang.SanPhamId == item.SanPhamId).ToList();

				var soluongconlai = sp.LoHangs.Sum(u => u.SoLuong) - chitiet.Sum(c => c.SoLuong);

				if (soluongconlai < item.SoLuong)
				{
					return new { IsSuccess = false, Message = $"Số lượng {sp.TenSp} không thể đáp ứng yêu cầu" };
				}

				foreach (var ct in sp.LoHangs)
				{
					if (item.SoLuong <= 0) break;

					var detail = ChiTiets.Where(c => c.LoHang.SanPhamId == ct.SanPhamId && c.LoHangId == ct.LoHangId).ToList();
					var tonkho = ct.SoLuong - detail.Sum(d => d.SoLuong);
					if (tonkho <= 0) continue;

					var dt = new ChiTietDonXuat
					{
						LoHangId = ct.LoHangId,
						SoLuong = item.SoLuong < tonkho ? item.SoLuong : tonkho
					};

					item.SoLuong = item.SoLuong < tonkho ? 0 : item.SoLuong - tonkho;
					hdx.ChiTietDonXuats.Add(dt);
				}
			}

			try
			{
				await _context.HoaDonXuats.AddAsync(hdx);
				var result = await _context.SaveChangesAsync();
				if (result > 0) return new { IsSuccess = true, Message = "Thêm hoá đơn xuất thành công" };
				return new { IsSuccess = false, Message = "Thêm hoá đơn xuất thất bại" };
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("Lỗi: " + ex);
				return new { IsSuccess = false, Message = "Lỗi xử lý db" };
			}
		}

		public async Task<dynamic> Delete(int Id)
		{
			try
			{
				var item = await _context.HoaDonXuats
						.FirstOrDefaultAsync(h => h.HoaDonXuatId == Id);

				if (item == null) return new { IsSuccess = false, Message = "Không tìm thấy đối tượng" };


				_context.HoaDonXuats.Remove(item);

				var result = await _context.SaveChangesAsync();

				if (result > 0) return new { IsSuccess = true, Message = "Xoá hoá đơn xuất thành công" };
				return new { IsSuccess = false, Message = "Xoá thất bại" };

			}
			catch (System.Exception)
			{
				return new { IsSuccess = false, Message = "Lỗi xử lý db" };
			}
		}

		public async Task<dynamic> FindAll()
		{
			return await _context.HoaDonXuats.Include(d => d.ChiTietDonXuats)
						.ToListAsync();
		}
	}
}