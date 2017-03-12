using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ORM;
using RefactorThis.GraphDiff;

namespace DAL
{
	public class CarRepository
	{
		private readonly DbContext context;

		public CarRepository(DbContext context)
		{
			this.context = context;
		}

		public void Update(Car car)
		{
			context.UpdateGraph(car, map => map.OwnedCollection(c => c.Wheels, with => with.OwnedCollection(col => col.Bolts)));
			context.SaveChanges();
		}

		public void Update<T>(int id, T newCar) where T : class
		{
			var entity = context.Set<T>().Find(id);
			Mapper.Map(newCar, entity);

			context.SaveChanges();
		}

		//context.Entry(entity).CurrentValues.SetValues(entity);
		public void AddRange(IEnumerable<Car> cars)
		{
			context.Set<Car>().AddRange(cars);
			context.SaveChanges();
		}

		public void Add(Car car)
		{
			context.Set<Car>().Add(car);
			context.SaveChanges();
		}

		public IEnumerable<Car> FindBy(Expression<Func<Car, bool>> predicate)
		{
			return context.Set<Car>().Where(predicate);
		}

		public IEnumerable<Car> FindByFunc(Func<Car, bool> predicate)
		{
			return context.Set<Car>().Where(predicate);
		}

		public void Create(Car car)
		{
			context.Set<Car>().Add(car);
			context.SaveChanges();
		}

		public IEnumerable<Car> GetAll()
		{
			return context.Set<Car>().AsNoTracking();
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

		public Car Get(int id)
		{
			return context.Set<Car>().Find(id);
		}

		public void Update<T>(int id, string name, T newCar) where T : class
		{
			var entity = context.Set<T>().Find(id, name);

			context.Entry(entity).CurrentValues.SetValues(newCar);
			context.SaveChanges();
		}

		public void UpdateRange(Car car)
		{
			//foreach (var car in cars)
			//{
			//	//context.Entry(car).State = EntityState.Detached;
			//	//context.Entry(car).State = EntityState.Modified;
			//	context.Set<Car>().Attach(car);
			//	context.Entry(car).State = EntityState.Modified;
			//}
			Car f = new Car { Id = 120 };
			//context.Set<Car>().Attach(f);
			//f.Name = "SuperNew";
			context.Entry(f).State = EntityState.Modified;

			context.SaveChanges();
		}

		public void DeleteAllRecords()
		{
			context.Set<Car>().RemoveRange(context.Set<Car>());
			context.SaveChanges();
		}

		public void AddOrUpdate(Wheel wheel)
		{
			context.Set<Wheel>().AddOrUpdate(wheel);
			context.SaveChanges();
		}

		public void AddOrUpdate(Car car)
		{
			context.Set<Car>().AddOrUpdate(car);
			//context.Set<Wheel>().AddOrUpdate(car.Wheels.ToArray());
			context.SaveChanges();
		}

		public void AddOrUpdate(IEnumerable<Car> cars)
		{
			context.Set<Car>().AddOrUpdate(cars.ToArray());
			context.SaveChanges();
		}

		//		public void UpdateWithff(IEnumerable<Car> updatedCars)
		//		{
		//			using (var dbContextTransactions = context.Database.BeginTransaction())
		//			{
		//				IEnumerable<Car> carsToDelete = context.Set<Car>().ToList().Except(updatedCars, new CarsComparer());

		//				foreach (var updatedCar in updatedCars)
		//				{
		//					context.Database.ExecuteSqlCommand(@"if exists (select Id from Cars where id = @nid)
		//                       update Cars
		//                         set Name = @name,Price=@price
		//                       where id = @nid
		//                    else
		//                       insert into Cars(Id, Name, Price)
		//                       values(@nid, @name, @price)",
		//					new SqlParameter("nid", updatedCar.Id),
		//					new SqlParameter("name", updatedCar.Name),
		//					new SqlParameter("price", updatedCar.Prices));
		//				}

		//				context.Set<Car>().RemoveRange(carsToDelete);

		//				context.SaveChanges();
		//				dbContextTransactions.Commit();
		//			}
		//		}

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
