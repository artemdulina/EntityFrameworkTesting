using AutoMapper;
using ORM;

namespace DAL.MapperProfiles
{
	public class DalProfile : Profile
	{
		public DalProfile()
		{
			//CreateMap<Car, Entities.Car>().PreserveReferences();
			//CreateMap<Entities.Car, Car>().PreserveReferences();

			//CreateMap<Wheel, Entities.Wheel>().PreserveReferences();
			//CreateMap<Entities.Wheel, Wheel>().PreserveReferences();

			//CreateMap<Bolt, Entities.Bolt>().ReverseMap();

			//CreateMap<Car, Entities.Car>().MaxDepth(1);
			//CreateMap<Entities.Car, Car>().MaxDepth(1);

			//CreateMap<Wheel, Entities.Wheel>().MaxDepth(1);
			//CreateMap<Entities.Wheel, Wheel>().MaxDepth(1);

			//CreateMap<Bolt, Entities.Bolt>().ReverseMap();

			CreateMap<Car, Entities.Car>();
			CreateMap<Wheel, Entities.Wheel>().ForMember(w => w.Car, opt => opt.Ignore());
			CreateMap<Bolt, Entities.Bolt>().ForMember(b => b.Wheel, opt => opt.Ignore());

			/*CreateMap<Car, Entities.Car>().AfterMap((src, dest) =>
			{
				foreach (Entities.Wheel destWheel in dest.Wheels)
				{
					destWheel.Car = dest;
					foreach (Entities.Bolt destWheelBolt in destWheel.Bolts)
					{
						destWheelBolt.Wheel = destWheel;
					}
				}
			});*/
		}
	}
}
