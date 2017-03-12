using System.Collections.Generic;
using System.Data.Entity;

namespace ORM
{
	public class MetricsInitializer : CreateDatabaseIfNotExists<DataContext>
	{
		protected override void Seed(DataContext context)
		{
			base.Seed(context);

			List<Car> carsToAdd = new List<Car>();
			for (int i = 0; i < 200; i++)
			{
				List<Wheel> wheels = new List<Wheel>();
				for (int j = 0; j < 800; j++)
				{
					wheels.Add(new Wheel
					{
						Size = j
					});
				}
				carsToAdd.Add(new Car
				{
					Name = "SuperBolt" + i,
					Wheels = wheels
				});
			}

			context.Cars.AddRange(carsToAdd);
			context.SaveChanges();
		}
	}
}
