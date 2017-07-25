using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	public class InvalidEmployeeException : System.Exception
	{
		public InvalidEmployeeException(string message)
			: base(message)
		{
			
		}
	}
}
