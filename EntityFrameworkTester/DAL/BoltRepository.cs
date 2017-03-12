using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using ORM;

namespace DAL
{
	public class BoltRepository
	{
		private readonly DbContext context;

		public BoltRepository(DbContext context)
		{
			this.context = context;
		}

		public Bolt Get(int id)
		{
			return context.Set<Bolt>().Find(id);
		}

		public void Update(Bolt bolt)
		{
			//Wheel wheel = context.Set<Wheel>().Find(2);
			//bolt.Wheel = wheel;
			context.Set<Bolt>().Where(b => b.Name == "SuperBolt").Update(t => new Bolt { Name = "UpdatedBolt" });
			context.SaveChanges();
		}
	}
}
