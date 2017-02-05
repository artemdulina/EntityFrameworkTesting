using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using ORM;

namespace DAL
{
    public class CarRepository
    {
        private readonly DbContext context;

        public CarRepository(DbContext context)
        {
            this.context = context;

            //context.Database.Log = Console.WriteLine;
        }

        public void AddRange(IEnumerable<Car> cars)
        {
            context.Set<Car>().AddRange(cars);
            context.SaveChanges();
        }

        public void Create(Car car)
        {
            context.Set<Car>().Add(car);
            context.SaveChanges();
        }

        public IEnumerable<Car> GetAll()
        {
            IEnumerable<Car> cars = context.Set<Car>().ToList();
            return cars;
        }

        //much faster then simple RemoveRange
        public void HardDeleteAllRecords()
        {
            using (var db = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.Cars");
                //context.Database.ExecuteSqlCommand(@"if exists (select Id from Cars where id = @nid)
                //   update Cars
                //     set Name = @name,Price=@price
                //   where id = @nid
                //else
                //   insert into Cars(Id, Name, Price)
                //   values(@nid, @name, @price)",
                //   new SqlParameter("nid", 2004),
                //   new SqlParameter("name", "newname"),
                //   new SqlParameter("price", 123456));

                db.Commit();
            }

            int u = 0;
        }

        public void DeleteAllRecords()
        {
            context.Set<Car>().RemoveRange(context.Set<Car>());
            context.SaveChanges();
        }

        public void AddOrUpdate(Car car)
        {
            context.Set<Car>().AddOrUpdate(car);
            context.SaveChanges();
        }

        public void UpdateWithff(IEnumerable<Car> updatedCars)
        {
            using (var dbContextTransactions = context.Database.BeginTransaction())
            {
                IEnumerable<Car> carsToDelete = context.Set<Car>().ToList().Except(updatedCars, new CarsComparer());
                
                foreach (var updatedCar in updatedCars)
                {
                    context.Database.ExecuteSqlCommand(@"if exists (select Id from Cars where id = @nid)
                       update Cars
                         set Name = @name,Price=@price
                       where id = @nid
                    else
                       insert into Cars(Id, Name, Price)
                       values(@nid, @name, @price)",
                    new SqlParameter("nid", updatedCar.Id),
                    new SqlParameter("name", updatedCar.Name),
                    new SqlParameter("price", updatedCar.Price));
                }              
                
                context.Set<Car>().RemoveRange(carsToDelete);

                context.SaveChanges();
                dbContextTransactions.Commit();
            }
        }

        public void UpdateWiths(IEnumerable<Car> updatedCars)
        {
            IEnumerable<Car> carsToDelete = context.Set<Car>().ToList().Except(updatedCars, new CarsComparer());
            
            context.Set<Car>().AddOrUpdate(updatedCars.ToArray());
            
            context.Set<Car>().RemoveRange(carsToDelete);

            context.SaveChanges();
        }

        /// <summary>
        /// Remove all old date and then insert all new data
        /// </summary>
        public void UpdateWith(IEnumerable<Car> updatedCars)
        {
            context.Set<Car>().RemoveRange(context.Set<Car>());
            
            context.Set<Car>().AddRange(updatedCars);

            context.SaveChanges();
        }
    }
}
