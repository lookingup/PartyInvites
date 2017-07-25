using Epic.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcf
{
	[DataSynchronizationContract(ClientSynchronizationMode.OnServerOnly, DatabaseSynchronizationMode.Automatic)]
	public class CheeseList
	{
		[DataSynchronizationMember]
		public ObservableCollection<Cheese> Cheeses { get; internal set; }
	}
}
