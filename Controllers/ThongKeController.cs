using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PPDD.Services;

namespace PPDD.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ThongKeController : ControllerBase
	{
		private readonly IThongKeService _thongke;
		public ThongKeController(IThongKeService thongke)
		{
			_thongke = thongke;
		}

		[HttpGet("thong-ke-theo-khoang-thoi-gian")]
		public async Task<IActionResult> Get(string NgayBd, string NgayKt)
		{
			Console.WriteLine(NgayBd + NgayKt);
			return Ok(new { NgayBD = NgayBd, NgayKT = NgayKt, Items = await _thongke.ThongKe(NgayBd, NgayKt) });
		}

		[HttpGet("thong-ke-chi-tiet-theo-khoang-thoi-gian")]
		public async Task<IActionResult> GetDetail(string NgayBd, string NgayKt)
		{
			return Ok(new { NgayBD = NgayBd, NgayKT = NgayKt, Items = await _thongke.ThongKe(NgayBd, NgayKt, true) });
		}

		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}