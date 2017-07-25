using Epic.Core.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcf
{
	public class CheeseFactory : IResultListClassFactory<Cheese>
	{
		public Cheese Create(Result r)
		{
			Cheese cheese = new Cheese();
			decimal price;
			ResultList cItems = new ResultList(r.Value, ResultType.Subfield);
			cheese.Id = cItems.NextPiece();
			cheese.Name = cItems.NextPiece();
			decimal.TryParse(cItems.NextPiece(), out price);
			cheese.Price = price;
			return cheese;
		}
	}
}
