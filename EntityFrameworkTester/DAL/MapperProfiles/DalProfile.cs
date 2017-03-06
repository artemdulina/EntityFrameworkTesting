using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ORM;

namespace DAL.MapperProfiles
{
	public class DalProfile : Profile
	{
		public DalProfile()
		{
			CreateMap<Wheel, Entities.Wheel>().PreserveReferences().ReverseMap();
			CreateMap<Car, Entities.Car>().PreserveReferences().ReverseMap();
			CreateMap<Bolt, Entities.Bolt>().PreserveReferences().ReverseMap();
		}
	}
}
