using Epic.Core.Attributes;
using Epic.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcf
{
	[DataSynchronizationContract(ClientSynchronizationMode.OnServerOnly, DatabaseSynchronizationMode.Automatic)]
	public class Cheese
	{
		[DataSynchronizationMember]
		public string Name { get; internal set; }

		[DataSynchronizationMember]
		public string Id { get; internal set; }

		[DataSynchronizationMember]
		public decimal Price { get; internal set; }

		[DataSynchronizationMember]
		public string Weight { get; internal set; }

		[DataSynchronizationMember]
		public Category Country { get; set; }

		public override string ToString()
		{
			if (Name == null || Name.Length == 0) return "";
			string ret = Name + ", " + Id;
			if (Country != null) ret += ", " + Country.Title;
			if (Price > 0 && Weight != null)
			{
				ret += " ($" + Price + " for " + Weight + "oz)";
			}
			return ret;
		}
	}
}
