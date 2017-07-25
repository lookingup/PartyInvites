using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	public class Company
	{
		private OrderedSet<Employee> _employees = new OrderedSet<Employee>();
		public string Name;
		private List<string> _duplicateAddAttempts = new List<string>();
		private DuplicateAddAttemptedEventHandler _duplicateAddAttempted;
		public event DuplicateAddAttemptedEventHandler DuplicateAddAttempted
		{
			add
			{
				_duplicateAddAttempted += value;
			}
			remove
			{
				_duplicateAddAttempted -= value;
			}
		}

		public IEnumerable<string> DuplicateAttemptsTrace
		{
			get
			{
				foreach (string duplicateMsg in _duplicateAddAttempts)
				{
					yield return duplicateMsg;
				}
			}
		}

		public Company(string name)
		{
			Name = name;
			_employees.DuplicateAddAttempted += DuplicateEmployeeAddAttempted;
		}

		public void Add(Employee e)
		{
			_employees.Add(e);
		}

		private void DuplicateEmployeeAddAttempted(object sender, DuplicateAddAttemptedEventArgs args)
		{
			_duplicateAddAttempts.Add(string.Format("Duplicate add of {0} attempted on {1}",
					args.Duplicate, DateTime.Now));
			RaiseDuplicateAddAttempted(args.Duplicate);
		}

		private void RaiseDuplicateAddAttempted(object duplicate)
		{
			DuplicateAddAttemptedEventHandler eventHandlers = _duplicateAddAttempted;
			if (eventHandlers != null)
			{
				eventHandlers(this, new DuplicateAddAttemptedEventArgs(duplicate));
			}
		}
	}
}
