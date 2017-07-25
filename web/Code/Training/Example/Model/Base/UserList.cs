using System.Collections.Generic;
using System.Xml.Serialization;

namespace Epic.Training.Example
{
	/// <summary>
	/// Class used for the serialization of lists of users to XML
	/// </summary>
	[XmlRoot(Namespace="Epic.Training.Example.Users")]
	public class UserList
	{
		/// <summary>
		/// The list of users
		/// </summary>
		[XmlElement("User")]
		public List<BigMouseUser> Users { get; set; }
        public static string FileName { get; set; }
	}
}
