using System;
using System.Web.Hosting;

namespace Epic.Training.Example.Web.Pages
{
	public class Global : System.Web.HttpApplication
	{

		/// <summary>
		/// The root location of all XML files.
		/// </summary>
		public static readonly string s_AppDataPath = HostingEnvironment.MapPath("~/App_Data/");

		/// <summary>
		/// Root file name of all cheese XML files.
		/// </summary>
		public static readonly string s_CheeseFileNameRoot = s_AppDataPath + "Cheeses";

		/// <summary>
		/// Root of cheese XML files.
		/// </summary>
		public static string CheeseFileName
		{
			get
			{
				string country = "";
				return s_CheeseFileNameRoot + country + ".xml";
			}
		}

		void Application_Start(object sender, EventArgs e)
		{
			// Code that runs on application startup

		}

		void Application_End(object sender, EventArgs e)
		{
			//  Code that runs on application shutdown

		}

		void Application_Error(object sender, EventArgs e)
		{
			// Code that runs when an unhandled error occurs

		}

		void Session_Start(object sender, EventArgs e)
		{
			// Code that runs when a new session is started

		}

		void Session_End(object sender, EventArgs e)
		{
			// Code that runs when a session ends. 
			// Note: The Session_End event is raised only when the sessionstate mode
			// is set to InProc in the Web.config file. If session mode is set to StateServer 
			// or SQLServer, the event is not raised.

		}

	}
}
