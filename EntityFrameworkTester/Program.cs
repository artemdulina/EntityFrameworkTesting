using System;
using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.MapperProfiles;
using ORM;
using Bolt = ORM.Bolt;

namespace EntityFrameworkTester
{
	class Program
	{
		static void Main(string[] args)
		{
			Mapper.Initialize(cfg => cfg.AddProfile(new DalProfile()));

			DataContext context = new DataContext();
			BoltRepository boltRepository = new BoltRepository(context);

			Bolt bolt = boltRepository.Get(1);
			Console.WriteLine(bolt.Name);

			DAL.Entities.Bolt dalBolt = Mapper.Map<Bolt, DAL.Entities.Bolt>(bolt);

			if (dalBolt.Wheel == null)
			{
				dalBolt.Wheel = new DAL.Entities.Wheel
				{
					Size = 1994
				};
			}
			else
			{
				dalBolt.Wheel.Size = 199999;
			}

			if (dalBolt.Wheel.Car == null)
			{
				dalBolt.Wheel.Car = new DAL.Entities.Car
				{
					Name = "Bugatti"
				};
			}
			else
			{
				dalBolt.Wheel.Car.Name = "Audi";
			}

			Bolt ormBolt = Mapper.Map<DAL.Entities.Bolt, Bolt>(dalBolt);

			boltRepository.Update(ormBolt);
		}
	}
}
