using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PPDD.DTO;
using PPDD.Models;
using PPDD.Services;

namespace QLKho.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HoaDonXuatController : ControllerBase
	{
		private readonly IHoaDonXuatService _hoadon;

		public HoaDonXuatController(IHoaDonXuatService hoadon)
		{
			_hoadon = hoadon;
		}

		[HttpPost]
		public async Task<IActionResult> NewHd([FromBody] TaoHoaDonXuat item)
		{
			var result = await _hoadon.AddAsync(item);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> FindAll()
		{
			return Ok(await _hoadon.FindAll());
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok(await _hoadon.Delete(id));
		}


		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

	}
}