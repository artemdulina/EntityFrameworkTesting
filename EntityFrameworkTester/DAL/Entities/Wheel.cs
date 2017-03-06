using System.Collections.Generic;

namespace DAL.Entities
{
	public class Wheel
	{
		public int Id { get; set; }

		public double Size { get; set; }

		public Car Car { get; set; }

		public List<Bolt> Bolts { get; set; }

		public override string ToString()
		{
			return string.Format("Id = {0}, Size = {1}", Id, Size);
		}
	}
}
