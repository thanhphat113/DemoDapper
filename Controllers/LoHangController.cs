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
	public class LoHangController : ControllerBase
	{
		private readonly ILoHangService _lohang;

		public LoHangController(ILoHangService lohang)
		{
			_lohang = lohang;
		}

		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			return Ok(await _lohang.LayDanhSachLoHangCuaSP(id));
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