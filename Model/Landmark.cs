using System.Collections.Generic;

namespace Model
{
	public class Landmark
	{
		public string Name { get; set; }
		public List<Photo> Photos { get; set; }
		public string Area { get; set; }
		public double Rating { get; set; }
	}
}