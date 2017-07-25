using Epic.Core.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcf
{
	class Program
	{
		static void Main(string[] args)
		{
			Connection connection = Connection.Create("FNDLABDVA", CacheLicensingModel.User);
			connection.Login("1", "epic");

			////v2.0 - get cheeses in price range (BulkRPC)
			////get inputs
			//Console.WriteLine("Enter lower and upper range of prices: ");
			//string strLprice = Console.ReadLine();
			//string strUprice = Console.ReadLine();
			//float lprice, uprice;
			//float.TryParse(strLprice, out lprice);
			//float.TryParse(strUprice, out uprice);
			//if (lprice > uprice) return;

			////execute bulkRPC
			//FieldList request = new FieldList() { strLprice, strUprice };
			//string result = null;
			//connection.BulkRpc("d GetCheesesInPriceRange^XCH", out result, request);
			//ResultList resultList = new ResultList(result);
			//ObservableCollection<Cheese> cheesesInRange;
			//cheesesInRange = resultList.ToObservableCollection<Cheese, CheeseFactory>();
			//foreach (Cheese cheese in cheesesInRange)
			//{
			//	Console.WriteLine(cheese.ToString());
			//}

			//v1.0 - get cheese by country (ECF Command)
			//get inputs
			Console.Write("Enter country ID: ");
			string country = Console.ReadLine();
			//execute command
			Command<Request, CheeseList> cmd = new Command<Request, CheeseList>("Train.dmathew.GetCheeses", connection);
			Request req = new Request();
			req.CountryValue = country;
			cmd.Request = req;
			cmd.Execute();
			//output results
			foreach (Cheese cheese in cmd.Response.Cheeses)
			{
				Console.WriteLine(cheese.ToString());
			}

			//epilogue
			connection.Release();
			Console.WriteLine("<Press any key to continue>");
			Console.ReadKey();
		}
	}
}
