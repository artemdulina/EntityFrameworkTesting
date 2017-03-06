using System.Collections.Generic;

namespace DAL.Entities
{
	public class Car
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<Wheel> Wheels { get; set; }

		public override string ToString()
		{
			return string.Format("Id = {0}, Name = {1}", Id, Name);
		}
	}
}
