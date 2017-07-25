using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Epic.Training.Base;

namespace Epic.Training.CompanySim.Text
{
	class Program
	{
		static void Main(string[] args)
		{
			//EmpTest();
			//CmplxTest();
			//ExecutiveTest();
			//DevTest();
			//StackTest();
			//OrderedSetTest();
			//RegexTest();
			//ExceptionTest();
			//CompanyTest();
			StreamTest();
			Simple s = new Simple();
			Console.ReadKey();
		}

		private static void StreamTest()
		{
			FileStream fs;
			using(fs = new FileStream("asdf.txt", FileMode.Create))
			using (StreamWriter w = new StreamWriter(fs))
			{
				for (int i = 1; i <= 10; i++)
					w.WriteLine(i);
				w.Flush();
			}
			//using (fs = new FileStream("App.config.xml", FileMode.Open, FileAccess.Read))
			using (StreamReader r = File.OpenText("App.config.xml")) 
			{
				//while (!r.EndOfStream)
				//	Console.WriteLine(r.ReadLine());
				string xml = r.ReadToEnd();
				Console.WriteLine(xml);
			}
		}

		private static void CompanyTest() 
		{
			Company c = new Company("UltraSoft");
			Developer d1 = new Developer("b17806");
			Developer d2 = new Developer("a806");
			Developer d3 = new Developer("a806");
			Developer d4 = new Developer("b17806");

			c.DuplicateAddAttempted += DisplayWarningMessage;
			c.Add(d1);
			c.Add(d2);
			c.Add(d3);
			c.Add(d4);

			foreach (string traceMsg in c.DuplicateAttemptsTrace)
			{
				Console.WriteLine(traceMsg);
			}
		}

		private static void DisplayWarningMessage(object sender, DuplicateAddAttemptedEventArgs args)
		{
			Console.WriteLine("Employee #{0} already present in {1}", ((Employee)args.Duplicate).ID, 
				((Company)sender).Name);
		}

		private static void ExceptionTest()
		{
			Developer d;
			try
			{
				d = new Developer("12345");
			}
			catch (InvalidEmployeeException e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				//de-allocate d
				Console.WriteLine("All done!");
			}
		}

		private static void RegexTest()
		{
			string badSSN = "123-456-1234", goodSSN = "123-45-6789";
			string delimStr = "one,two,three Liberty Associates, Inc.";
			string input = "isisis";
			Regex ssnRegex = new Regex(@"^\d{3}\-\d{2}\-\d{4}$");
			Regex delimRegex = new Regex(@" |, |,");
			Regex greedyRegex = new Regex(@"(is)+");
			Regex lazyRegex = new Regex(@"(is)+?");
			MatchCollection greedy = greedyRegex.Matches(input);
			MatchCollection lazy = lazyRegex.Matches(input);
			StringBuilder sb = new StringBuilder();
			int id = 1;

			//throw new Exception("voila");
			if (!ssnRegex.IsMatch(badSSN)) Console.WriteLine("{0} is a bad SSN", badSSN);
			if (ssnRegex.IsMatch(goodSSN)) Console.WriteLine("{0} is a good SSN", goodSSN);
			foreach (string substr in delimRegex.Split(delimStr))
				sb.AppendFormat("{0}: {1}\n", id++, substr);
			Console.WriteLine("{0}", sb);

			Console.WriteLine("Greedy matches:");
			DisplayMatches(greedy);
			Console.WriteLine("Lazy matches:");
			DisplayMatches(lazy);
		}

		private static void DisplayMatches(MatchCollection matches)
		{
			Console.WriteLine("Number of matches: " + matches.Count);
			foreach (Match m in matches)
				Console.WriteLine(m.Value);
		}

		private static void CmplxTest()
		{
			Complex c1 = new Complex(1.0, 2.568);
			Complex? c2 = new Complex(-2.5, -10.8);
			Console.WriteLine(c1 + c2);
		}

		private static void OrderedSetTest()
		{
			OrderedSet<int> set = new OrderedSet<int>() {3, 1, 2};
			set.DuplicateAddAttempted += ReportDuplicateAttempt;
			set.Add(3);
			foreach (int num in set)
				Console.Write(num + " ");
		}

		private static void ReportDuplicateAttempt(object sender, DuplicateAddAttemptedEventArgs args)
		{
			Console.WriteLine("Attempt to add duplicate!");
		}

		private static void StackTest()
		{
			Stack<string> cards = new Stack<string>();
			string[] strArr = new string[3];
			cards.Push("Ace of hearts");
			cards.Push("Queen of spades");
			//strArr = cards.ToArray();
			cards.CopyTo(strArr, 1);
			foreach (string s in strArr)
				Console.WriteLine(s);
		}

		private static void DevTest()
		{
			Employee e = new Developer("17806");
			Developer d = (Developer) e;
			e.DoWork();
			d.AddProject("Widgetize AP note report");
			d.AddProject("Perf rep");
			d.AddProject("Recruit");
			d.AddProject("Mentor");
			d.AddProjects("Evaluate C exercises", "PL rec suggestions");
			d.PrintProjects();
		}

		private static void ExecutiveTest()
		{
			Employee e = new Executive("1");
			Console.WriteLine("Executive - ID: {0}, Base salary: {1}", e.ID, e.BaseSal);
			e.DoWork();
		}

		/// <summary>
		/// Method to test the Employee class
		/// </summary>
		//private static void EmpTest()
		//{
		//	Employee emp1 = new Employee("17806");
		//	Employee emp2, emp3;
		//	emp3 = null;
		//	emp1.App = App.IP;
		//	emp1.BaseSal = 10000;
		//	emp1.Name = "djm";
		//	emp1.Tenure = 3;
		//	emp2 = new Employee(emp1);
		//	Console.WriteLine("Emp1: {0}", emp1.Name);
		//	Console.WriteLine("  Monthly payment: {0:0.00}", Employee.CalculateMonthlyPayment(emp1.BaseSal));
		//	emp1.GetRaise(1000);
		//	Console.WriteLine("  Monthly payment after raise: {0:0.00}", Employee.CalculateMonthlyPayment(emp1.BaseSal));
		//	Console.WriteLine("Emp2: {0}", emp2.Name);
		//	Console.WriteLine("Emp1 == Emp2 ? {0}", emp1 == emp2);
		//	emp2.ID = "99999";
		//	Console.WriteLine("[After ID change] Emp1 == Emp2 ? {0}", emp1 == emp2);
		//	Console.WriteLine("Emp1 == null ? {0}", emp1 == emp3);
		//	Console.WriteLine("null == Emp2 ? {0}", emp3 == emp2);
		//	Console.WriteLine("null == null ? {0}", emp3 == emp3);
		//}
	}

	struct Complex
	{
		public double re { get; set; }
		public double im { get; set; }
		public Complex(double a, double b) //a + ib
			: this()
		{
			this.re = a;
			this.im = b;
		}
		public override string ToString()
		{
			return string.Format("{0:0.00} + {1:0.00}i", re, im); 
		}
		public static Complex operator +(Complex lhs, Complex rhs)
		{
			return new Complex(lhs.re + rhs.re, lhs.im + rhs.im);		
		}
	}

	class Simple
	{
		private int field1;
		//default constructor exists as long as no other constructor is explicitly defined
		//public Simple(int f)
		//{
		//	this.field1 = f;
		//}
	}
}
