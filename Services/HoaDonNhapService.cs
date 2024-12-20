using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PPDD.Models;

namespace PPDD.Services
{
	public interface IHoaDonNhapService
	{
		Task<dynamic> AddAsync(HoaDonNhap invoice);
		Task<dynamic> FindAll();
		Task<dynamic> Delete(int Id);
	}

	public class HoaDonNhapService : IHoaDonNhapService
	{
		private readonly QlkhoContext _context;

		public HoaDonNhapService(QlkhoContext context
								)
		{
			_context = context;
		}

		public async Task<dynamic> AddAsync(HoaDonNhap invoice)
		{
			if (invoice.LoHangs == null || !invoice.LoHangs.Any())
			{
				return new { IsSuccess = false, Message = "Không thể tạo một hoá đơn rỗng" };
			}

			try
			{
				await _context.HoaDonNhaps.AddAsync(invoice);
				var result = await _context.SaveChangesAsync();
				if (result > 0) return new { IsSuccess = true, Message = "Thêm hoá đơn nhập thành công" };
				return new { IsSuccess = false, Message = "Thêm hoá đơn nhập thất bại" };
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
				var item = await _context.HoaDonNhaps.FirstOrDefaultAsync(h => h.HoaDonNhapId == Id);

				_context.HoaDonNhaps.Remove(item);

				var result = await _context.SaveChangesAsync();

				if (result > 0) return new { IsSuccess = true, Message = "Xoá hoá đơn nhập thành công" };
				return new { IsSuccess = false, Message = "Xoá thất bại" };

			}
			catch (System.Exception)
			{
				return new { IsSuccess = false, Message = "Lỗi xử lý db" };
			}
		}

		public Task<dynamic> FindAll()
		{
			throw new NotImplementedException();
		}
	}

}