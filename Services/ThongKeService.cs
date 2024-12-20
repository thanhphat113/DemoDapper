using System.Data;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PPDD.Models;

namespace PPDD.Services
{
	public interface IThongKeService
	{
		Task<dynamic> ThongKe(string NgayBD, string NgayKT, bool isDetail = false);
	}

	public class ThongKeService : IThongKeService
	{
		private readonly IMapper _mapper;
		private readonly QlkhoContext _context;
		public ThongKeService(QlkhoContext context, IMapper mapper)
		{
			_mapper = mapper;
			_context = context;
		}
		public async Task<dynamic> ThongKe(string NgayBD, string NgayKT, bool isDetail = false)
		{
			using (var connection = _context.Database.GetDbConnection())
			{
				var parameters = new { NGAYBD = NgayBD, NGAYKT = NgayKT };
				var result = await connection.QueryAsync<ThongKe>(isDetail ? "THONGKECHITIET" : "THONGKE", parameters, commandType: CommandType.StoredProcedure);
				return result;
			}
		}
	}
}