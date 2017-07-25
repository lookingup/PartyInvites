using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	interface IDisposable
	{
		void Dispose();
		void Dispose(bool Disposing);
	}
}
