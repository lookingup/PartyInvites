using System.Xml.Serialization;

namespace Epic.Training.Example
{
	/// <summary>
	/// Class representing a user of the Big Mouse Cheese Factory
	/// </summary>
	public class BigMouseUser
	{
		/// <summary>
		/// The user roles available in this application
		/// </summary>
		public enum UserRole
		{
			Customer,
			Administrator
		}

		[XmlAttribute]
		public string Name { get; set; }

		[XmlAttribute]
		public string Email { get; set; }
		
		[XmlAttribute]
		public string Password { get; set; }

		[XmlAttribute]
		public UserRole Role { get; set; }

		[XmlAttribute]
		public string Country { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public BigMouseUser() {}

		/// <summary>
		/// Constructor including the name, password and role
		/// </summary>
		/// <param name="name">The user's name</param>
		/// <param name="password">The user's password</param>
		/// <param name="role">The user's role</param>
		public BigMouseUser(string name, string password, UserRole role)
		{
			Name = name;
			Password = password;
			Role = role;
		}
	}
}
