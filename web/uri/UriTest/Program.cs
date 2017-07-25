using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string path1 = @"\\epic-nfs\3day\dmathew\asdf.txt";
			string path2 = "C:/wilma.log/{1}\"";
			//UriBuilder linkBuilder1 = new UriBuilder(path1);
			//UriBuilder linkBuilder2 = new UriBuilder(path2);
			//Uri link1 = linkBuilder1.Uri;
			//Uri link2 = linkBuilder2.Uri;
			Uri link1, link2;

			Uri.TryCreate(path1, UriKind.Absolute, out link1);
			Console.WriteLine("{0} -> {1}", path1, link1.ToString());
			if (Uri.TryCreate(path2, UriKind.Absolute, out link2))
			{
				//if(Uri.IsWellFormedUriString())
				Console.WriteLine("Try 1: {0} -> {1}", path2, link2.ToString());
			}
			else if (Uri.TryCreate("http://" + path2, UriKind.Absolute, out link2))
			{
				Console.WriteLine("Try 2: {0} -> {1}", path2, link2.ToString());
			}
			else
			{
				UriBuilder linkBuilder2 = new UriBuilder(path2);
				link2 = linkBuilder2.Uri;
				Debug.Print("path2 was ill-formed! What about this?: {0}", link2.ToString());
			}
			Console.WriteLine();
			Console.WriteLine("Uri.EscapeUriString(path1) = {0}", Uri.EscapeUriString(path1));
			Console.WriteLine("Uri.EscapeDataString(path1) = {0}", Uri.EscapeDataString(path1));
			
			Console.ReadKey(true);
		}
	}
}
