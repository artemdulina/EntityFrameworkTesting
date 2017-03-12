using System.Collections.Generic;

namespace DAL.Entities
{
	public class Wheel
	{
		public int Id { get; set; }

		public double Size { get; set; }

		public Car Car { get; set; }

		public List<Bolt> Bolts { get; set; }
	}
}
