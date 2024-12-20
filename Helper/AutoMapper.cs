using AutoMapper;
using PPDD.DTO;
using PPDD.Models;

namespace PPDD.Helper;
public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<LoHang, LoHangConLai>();
	}
}