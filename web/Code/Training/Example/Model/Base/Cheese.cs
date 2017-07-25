using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Epic.Training.Example
{
	/// <summary>
	/// Class used to represent cheese objects
	/// </summary>
	[DataContract]
	public class Cheese
	{
	    [XmlAttribute, DataMember]
		public string Name { get; set; }

		[XmlAttribute, DataMember]
		public int Item { get; set; }

		[XmlAttribute, DataMember]
		public string ImagePathSmall { get; set; }

		[XmlAttribute, DataMember]
		public string ImagePathLarge { get; set; }

		[XmlAttribute, DataMember]
		public string Description { get; set; }

		[XmlAttribute, DataMember]
		public double Weight { get; set; }

		[XmlAttribute, DataMember]
		public double Price { get; set; }
	}
}