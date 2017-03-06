using System;
using DAL;
using ORM;

namespace AnotherTester
{
	class Program
	{
		static void Main(string[] args)
		{
			DataContext context = new DataContext();
			CarRepository carRepository = new CarRepository(context);

			Car car = carRepository.Get(120);
			Console.WriteLine(car);
		}
	}
}
