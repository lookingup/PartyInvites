using System.IO;
using System.Xml.Serialization;
using System;

//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   

namespace Epic.Training.Core.Xml
{
	/// <summary>
	/// Class that adds XML storage functionality to child classes
	/// </summary>
	/// <typeparam name="L">The type of the objects that wll be serialized to/from XML</typeparam>
	public static class XmlLoader<L> where L: new()
	{
		/// <summary>
		/// Load the XML file from the given relative file name
		/// </summary>
		/// <param name="filename">The relative path to the XML file</param>
		/// <returns>The loaded object</returns>
		public static L LoadItems(string filename)
		{
			L list = default(L);

			XmlSerializer xs = new XmlSerializer(typeof(L));

			using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				list = (L)xs.Deserialize(fs);
			}

			return list;
		}

		/// <summary>
		/// Replace the given object with the provided object in the given filename.
		/// </summary>
		/// <param name="filename">Relative path to the XML file</param>
		/// <param name="list">Object to save</param>
		public static void ReplaceList(string filename, L list)
		{
			string fn = filename;

			XmlSerializer xs = new XmlSerializer(typeof(L));

			using (FileStream fs = File.Open(fn, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				xs.Serialize(fs, list);
			}

		}

		/// <summary>
		/// Create an XML file for a new object of type L
		/// </summary>
		/// <param name="filename">Relative path to the XML file</param>
		public static void CreateList(string filename)
		{
			ReplaceList(filename, new L());
		}

		/// <summary>
		/// Delete an XML file
		/// </summary>
		/// <param name="filename">Relative path to XML file</param>
		public static void DeleteList(string filename)
		{
			File.Delete(filename);
		}
	}
}