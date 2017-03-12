using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using DAL;
using DAL.MapperProfiles;
using AutoMapper;
using Mapster;
using ORM;

namespace EntityFrameworkTester
{
	public class A
	{
		public int X { get; set; }
	}
	public class B
	{
		public int X { get; set; }
	}
	class DalMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.ForType<Car, DAL.Entities.Car>();
			config.ForType<DAL.Entities.Car, Car>();

			config.ForType<Wheel, DAL.Entities.Wheel>().Ignore(w => w.Car);
			config.ForType<DAL.Entities.Wheel, Wheel>();

			config.ForType<Bolt, DAL.Entities.Bolt>().Ignore(b => b.Wheel);
			config.ForType<DAL.Entities.Bolt, Bolt>();

			//config.ForType<Car, DAL.Entities.Car>().PreserveReference(true);
			//config.ForType<DAL.Entities.Car, Car>().PreserveReference(true);

			//config.ForType<Wheel, DAL.Entities.Wheel>().PreserveReference(true);
			//config.ForType<DAL.Entities.Wheel, Wheel>().PreserveReference(true);

			//config.ForType<Bolt, DAL.Entities.Bolt>();
			//config.ForType<DAL.Entities.Bolt, Bolt>();
		}
	}
	class Program
	{
		public static DAL.Entities.Bolt MapFrom(Bolt bolt)
		{
			DAL.Entities.Bolt mappedBolt = new DAL.Entities.Bolt
			{
				Id = bolt.Id,
				Name = bolt.Name,
			};
			return mappedBolt;
		}
		public static DAL.Entities.Wheel MapFrom(Wheel wheel)
		{
			DAL.Entities.Wheel mappedWheel = new DAL.Entities.Wheel
			{
				Id = wheel.Id,
				Size = wheel.Size,
			};
			return mappedWheel;
		}
		public static DAL.Entities.Car MapFrom(Car car)
		{
			DAL.Entities.Car mappedCar = new DAL.Entities.Car
			{
				Id = car.Id,
				Name = car.Name,
			};
			return mappedCar;
		}
		public static List<DAL.Entities.Bolt> MapFrom(List<Bolt> bolts)
		{
			List<DAL.Entities.Bolt> mappedBolts = new List<DAL.Entities.Bolt>();
			foreach (Bolt bolt in bolts)
			{
				mappedBolts.Add(MapFrom(bolt));
			}

			return mappedBolts;
		}
		public static List<DAL.Entities.Wheel> MapFrom(List<Wheel> wheels)
		{
			List<DAL.Entities.Wheel> mappedwheels = new List<DAL.Entities.Wheel>();
			foreach (Wheel wheel in wheels)
			{
				mappedwheels.Add(MapFrom(wheel));
			}

			return mappedwheels;
		}
		public static List<DAL.Entities.Car> MapFromddddddd(List<Car> cars)
		{
			List<DAL.Entities.Car> mappedCars = new List<DAL.Entities.Car>();
			foreach (Car car in cars)
			{
				mappedCars.Add(MapFrom(car));
			}

			return mappedCars;
		}
		public static List<DAL.Entities.Car> MapFrom(List<Car> cars)
		{
			List<DAL.Entities.Car> mappedCars = new List<DAL.Entities.Car>();
			foreach (Car car in cars)
			{
				mappedCars.Add(MapFrom(car));
				List<DAL.Entities.Wheel> wheels = new List<DAL.Entities.Wheel>();
				foreach (Wheel carWheel in car.Wheels)
				{
					wheels.Add(MapFrom(carWheel));
					List<DAL.Entities.Bolt> bolts = new List<DAL.Entities.Bolt>();
					foreach (Bolt carWheelBolt in carWheel.Bolts)
					{
						bolts.Add(MapFrom(carWheelBolt));
					}
					wheels.Last().Bolts = bolts;
				}
				mappedCars.Last().Wheels = wheels;
			}

			return mappedCars;
		}

		static void Main(string[] args)
		{
			//TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly());
			Mapper.Initialize(cfg => cfg.AddProfile(new DalProfile()));
			//Mapper.Register<Car, DAL.Entities.Car>();
			//Mapper.Register<Wheel, DAL.Entities.Bolt>();
			//Mapper.Register<Bolt, DAL.Entities.Bolt>();

			//Mapper.Register<A, B>();
			//IEnumerable<A> a = new List<A> { new A { X = 6 } };
			//IEnumerable<B> b = a.Adapt<IEnumerable<B>>();


			DataContext context = new DataContext();
			CarRepository carRepository = new CarRepository(context);

			//var timer = Stopwatch.StartNew();
			//List<Car> cars = carRepository.GetAll().ToList();
			//List<DAL.Entities.Car> carsDal = cars.Adapt<List<DAL.Entities.Car>>();
			//timer.Stop();
			//Console.WriteLine(timer.ElapsedMilliseconds + "ms");
			//Console.WriteLine("Cars count: " + carsDal.Count);

			//var timer = Stopwatch.StartNew();
			//IEnumerable<Car> cars = carRepository.GetAll().ToList();
			//IEnumerable<DAL.Entities.Car> carsDal = Mapper.Map<IEnumerable<Car>, IEnumerable<DAL.Entities.Car>>(cars);
			//timer.Stop();
			//Console.WriteLine(timer.ElapsedMilliseconds + "ms");
			//Console.WriteLine("Cars count: " + carsDal.Count());

			var timer = Stopwatch.StartNew();
			List<Car> cars = carRepository.GetAll().ToList();
			List<DAL.Entities.Car> carsDal = MapFrom(cars);
			timer.Stop();
			Console.WriteLine(timer.ElapsedMilliseconds + "ms");
			Console.WriteLine("Cars count: " + carsDal.Count());

			//Car car = carRepository.Get(1);
			//DAL.Entities.Car dalCar = car.Adapt<DAL.Entities.Car>();
			//Car newCar = dalCar.Adapt<Car>();
			//newCar.Name = "Abrada";
			//newCar.Wheels[0].Size = 555;
			//newCar.Wheels[0].Bolts = new List<Bolt>
			//{
			//	new Bolt
			//	{
			//		Name = "Happiness"
			//	}
			//};
			//carRepository.Update(newCar);

			//Car car = carRepository.Get(1);
			//DAL.Entities.Car dalCar = Mapper.Map<Car, DAL.Entities.Car>(car);
			//Car newCar = Mapper.Map<DAL.Entities.Car, Car>(dalCar);
			//newCar.Name = "Abrada";
			//newCar.Wheels[0].Size = 555;
			//newCar.Wheels[0].Bolts = new List<Bolt>
			//{
			//	new Bolt
			//	{
			//		Name = "Happiness78"
			//	}
			//};
			//carRepository.Update(newCar);
		}
	}
}
