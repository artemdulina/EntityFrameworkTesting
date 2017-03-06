using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Bolt
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Wheel Wheel { get; set; }

		public override string ToString()
		{
			return string.Format("Id = {0}, Name = {1}", Id, Name);
		}
	}
}
