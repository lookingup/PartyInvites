using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassExamples
{
	class Program
    {
		enum Fruit { Apple, Banana, Orange, Peach, Pear, Strawberry }
		
		static void Main(string[] args)
        {
			Fruit fruit = GetFruit();
			string fruitName = Enum.GetName(typeof(Fruit), fruit);
			Console.WriteLine("Fruit# entered by user: {0}", fruitName);
			Console.ReadLine();
        }

		/// <summary>
		/// Hello world code; collapsible code regions.
		/// </summary>
		private static void HelloWorld()
		{
			#region Console output
			Console.WriteLine("Hello world!");
			Console.ReadKey();
			#endregion
		}

		/// <summary>
		/// Prompts user to enter name of a fruit and validates input.
		/// </summary>
		/// <returns>Corresponding Fruit enum index.</returns>
		private static Fruit GetFruit()
		{
			string fruitOptions = GetFruitOptions();
			Fruit myFruit = default(Fruit);
			bool isValid = false;
			string input = null;

			do
			{
				if (!string.IsNullOrWhiteSpace(input))
				{
					Console.WriteLine("That is not a Fruit.");
					Console.WriteLine("Enter the name of a fruit exactly as listed");
				}
				Console.WriteLine("Enter a fruit: ({0}):", fruitOptions);
				input = Console.ReadLine();
				if (Enum.TryParse<Fruit>(input, out myFruit))
				{
					isValid = Enum.IsDefined(typeof(Fruit), myFruit);
				}
			} while (!isValid);
			return myFruit;
		}

		/// <summary>
		/// Generates string of Fruit enum members.
		/// </summary>
		private static string GetFruitOptions()
		{
			string[] fruitOptionArray = Enum.GetNames(typeof(Fruit));
			for (int i = 0; i < fruitOptionArray.Length; i++)
			{
				fruitOptionArray[i] = String.Format("[{0}] {1}", i, fruitOptionArray[i]);
			}
			string fruitOptions = string.Join(",", fruitOptionArray);
			return fruitOptions;
		}
    }
}
