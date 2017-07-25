using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	public enum App { Amb, IP, Billing };
    public abstract class Employee : IDisposable
	{
		#region properties
		public string Name { get; set; }
		public string ID { get; set; }
		private int _tenure;

		public int Tenure
		{
			get { return _tenure; }
			set
			{
				if (value >= 0) 
				{
					_tenure = value;
				}
			}
		}

		public int BaseSal { get; set; }
		public App App;
		#endregion

		#region methods
		public static double CalculateMonthlyPayment(int sal)
		{
			return sal/12.0;
		}
		public void GetRaise(int increment)
		{
			this.BaseSal += increment;
		}
		public void DoWork()
		{
			Console.WriteLine("Working hard.");
			DoWorkCore();
		}
		protected virtual void DoWorkCore()
		{
			Console.WriteLine("...or hardly working?");
		}
		#endregion

		#region constructors
		public Employee(string ID) 
		{
			Regex firstAlpha = new Regex(@"^[A-Za-z]");
			if (!firstAlpha.IsMatch(ID))
			{
				throw new InvalidEmployeeException("ID doesn't start with alphabet");
			}
			this.ID = ID; 
		}
		public Employee(Employee srcEmp) 
		{ 
			this.Name = srcEmp.Name;
			this.ID = srcEmp.ID;
			this.Tenure = srcEmp.Tenure;
			this.BaseSal = srcEmp.BaseSal;
			this.App = srcEmp.App;
		}
		#endregion

		#region overloads and overrides
		public static bool operator ==(Employee lhs, Employee rhs)
		{
			return object.Equals(lhs, rhs);
		}

		public static bool operator !=(Employee lhs, Employee rhs)
		{
			return !(lhs == rhs);
		}

		/// <summary>
		/// object.Equals(a,b) calls this method if neither a or b are null.
		/// </summary>
		public override bool Equals(object rhs)
		{
			Employee e = rhs as Employee;
			if (e == null) 
			{
				return false;
			}
			return this.ID.Equals(e.ID);
		}

		/// <summary>
		/// Returns numeric value of ID. If ID is non-numeric, returns a random integer
		/// (since in that case, it shouldn't be equal to any other Employee).
		/// </summary>
		public override int GetHashCode()
		{
			int result;
			if (!int.TryParse(this.ID, out result))
			{
				Random r = new Random();
				return r.Next();
			}
			return result;
		}

		public override string ToString()
		{
			return string.Format("{0} - {1}", this.Name, this.ID);
		}
		#endregion

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Dispose(bool Disposing)
		{
			//nothing to dispose
		}

		~Employee()
		{
			Dispose(false);
		}
	}
}
