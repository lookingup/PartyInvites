using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Training.Base
{
	public class Developer:Employee,ITechnical
	{
		private string[] projects;  //queue of most recent 5
		private int projQHead;

		public Developer(string ID) : base(ID) 
		{
			projects = new string[5];
			projQHead = -1;
		}
		public void AddProject(string projName)
		{
			projQHead = (projQHead + 1) % 5;
			projects[projQHead] = projName;
		}
		public void AddProjects(params string[] projList)
		{
			foreach (string project in projList)
				AddProject(project);
		}

		/// <summary>
		/// Print all projects, starting with the oldest
		/// </summary>
		public void PrintProjects()
		{
			if (projQHead < 0) return;
			for (int i = projQHead + 1; ; i = (i + 1) % 5)
			{
				if ((projects[i] != null) && (projects[i].Length > 0)) Console.WriteLine(projects[i]);
				if (i == projQHead) break;
			}
		}
		public void WritingCode()
		{
			Console.WriteLine("Writing code.");
		}
		protected override void DoWorkCore()
		{
			this.WritingCode();
		}

		public new void Dispose(bool disposing)
		{
			if (disposing == true)
			{
				//destroy projects array
			}
		}
	}
}
