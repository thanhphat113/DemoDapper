using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PPDD.Models;
using PPDD.Services;

namespace QLKho.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HoaDonNhapController : ControllerBase
	{
		private readonly IHoaDonNhapService _hoadon;

		public HoaDonNhapController(IHoaDonNhapService hoadon)
		{
			_hoadon = hoadon;
		}

		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] HoaDonNhap item)
		{
			var result = await _hoadon.AddAsync(item);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok(await _hoadon.Delete(id));
		}
	}
}