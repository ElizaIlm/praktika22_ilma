using praktika22.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace praktika22.Data.ViewModell
{
	public class VMItems
	{
		public IEnumerable<Items> Items { get; set; }
		public IEnumerable<Categorys> Categorys { get; set; }
		public int SelectCategory = 0;

	}
}
