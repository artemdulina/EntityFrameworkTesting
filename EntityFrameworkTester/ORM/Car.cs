using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
	public class Car
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual List<Wheel> Wheels { get; set; }
	}
}
