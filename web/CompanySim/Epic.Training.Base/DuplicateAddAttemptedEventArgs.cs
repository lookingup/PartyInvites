using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	public class DuplicateAddAttemptedEventArgs : EventArgs
	{
		public object Duplicate { get; private set; }

		public DuplicateAddAttemptedEventArgs(object duplicate)
		{
			Duplicate = duplicate;
		}
	}
}
