using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	public class Executive:Employee
	{
		public Executive(string ID)
			: base(ID)
		{
			this.BaseSal = 3000;
		}
		protected override void DoWorkCore()
		{
			Console.WriteLine("Signing prospective customers");
		}
	}
}
