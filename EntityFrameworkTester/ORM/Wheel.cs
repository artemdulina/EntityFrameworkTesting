using System.Collections.Generic;

namespace ORM
{
	public class Wheel
	{
		public int Id { get; set; }

		public double Size { get; set; }

		public virtual Car Car { get; set; }

		public virtual List<Bolt> Bolts { get; set; }

		public override string ToString()
		{
			return string.Format("Id = {0}, Size = {1}", Id, Size);
		}
	}
}
