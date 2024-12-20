using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PPDD.DTO;
using PPDD.Models;

namespace PPDD.Services
{
	public interface ILoHangService
	{
		Task<dynamic> LayDanhSachLoHangCuaSP(int SanPhamID);
	}

	public class LoHangService : ILoHangService
	{
		private readonly QlkhoContext _context;
		private readonly IMapper _mapper;

		public LoHangService(QlkhoContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<dynamic> LayDanhSachLoHangCuaSP(int SanPhamID)
		{
			var ChiTiets = await _context.ChiTietDonXuats.Include(c => c.LoHang)
					.ToListAsync();

			var items = await _context.LoHangs.Where(h => h.SanPhamId == SanPhamID)
						.ToListAsync();

			var Result = new List<LoHangConLai>();
			foreach (var item in items)
			{
				var soluongxuat = ChiTiets.Where(c => c.LoHangId == item.LoHangId)
					.Sum(s => s.SoLuong);

				var conlai = _mapper.Map<LoHangConLai>(item);
				conlai.SoLuongConLai = item.SoLuong - soluongxuat;

				if (item.SoLuong - soluongxuat > 0) Result.Add(conlai);
			}

			return Result;
		}
	}
}