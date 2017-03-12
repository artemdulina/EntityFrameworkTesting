using System.Data.Entity;

namespace ORM
{
	public class DataContext : DbContext
	{
		public DataContext()
			: base("name=DataContext")
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>());
			Database.SetInitializer(new MetricsInitializer());
		}

		public DbSet<Car> Cars { get; set; }

		public DbSet<Wheel> Wheels { get; set; }
	
		public DbSet<Bolt> Bolts { get; set; }
	}
}
