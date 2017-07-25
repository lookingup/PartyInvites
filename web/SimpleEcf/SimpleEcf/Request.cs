using Epic.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcf
{
	[DataSynchronizationContract(ClientSynchronizationMode.OnServerOnly, DatabaseSynchronizationMode.Automatic)]
	public class Request
	{
		[DataSynchronizationMember]
		public string CountryValue { get; set; }
	}
}
