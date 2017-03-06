using System.Data.Entity;
using ORM;
using RefactorThis.GraphDiff;

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
			context.UpdateGraph(bolt, map => map.OwnedEntity(w => w.Wheel));
			context.UpdateGraph(bolt.Wheel, map => map.OwnedEntity(w => w.Car));
			context.SaveChanges();
		}
	}
}
