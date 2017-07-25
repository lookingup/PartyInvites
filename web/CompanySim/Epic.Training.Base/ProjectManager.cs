using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	class ProjectManager:Employee
	{
		public ProjectManager(string ID)
			: base(ID)
		{
			this.BaseSal = 2000;
		}
		protected override void DoWorkCore()
		{
			Console.WriteLine("On a go-live");
		}
	}
}
