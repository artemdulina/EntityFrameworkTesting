using System.Collections.Generic;
using System.Data.Entity;

namespace ORM
{
	public class MetricsInitializer : CreateDatabaseIfNotExists<DataContext>
	{
		protected override void Seed(DataContext context)
		{
			base.Seed(context);

			context.Bolts.Add(new Bolt
			{
				Name = "SuperBolt"
			});

			context.SaveChanges();
		}
	}
}
