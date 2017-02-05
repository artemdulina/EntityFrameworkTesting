using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using ORM;
using DAL;

namespace EntityFrameworkTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(ConfigurationManager.AppSettings["mainconfig"]);
            //Console.WriteLine(ConfigurationManager.AppSettings["extconfig"]);

            //NameValueCollection collection = (NameValueCollection)ConfigurationManager.GetSection("customSection");
            //Console.WriteLine(collection["mainconfig"]);
            //Console.WriteLine(collection["extconfig"]);

            DataContext context = new DataContext();
            CarRepository carRepository = new CarRepository(context);

            carRepository.DeleteAllRecords();
            // carRepository.HardDeleteAllRecords();

            List<Car> oldCars = new List<Car>();
            for (int i = 0; i < 1000; i++)
            {
                oldCars.Add(new Car { Id = i, Name = "oldBugatti" + i, Price = 1000000 });
            }
            carRepository.AddRange(oldCars);

            List<Car> updatedCars = new List<Car>();
            for (int j = 1000; j < 2000; j++)
            {
                updatedCars.Add(new Car { Id = j, Name = "updatedBugatti" + j, Price = 1000000 });
            }

            carRepository.UpdateWith(updatedCars);
        }
    }
}
