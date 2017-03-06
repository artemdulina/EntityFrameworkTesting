using System.Data.Entity.Migrations;

namespace ORM.Migrations
{

	internal sealed class Configuration : DbMigrationsConfiguration<ORM.DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;

			// register mysql code generator
			SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
		}

		protected override void Seed(ORM.DataContext context)
		{

		}
	}
}
