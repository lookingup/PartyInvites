using Epic.Training.Core.Xml;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;

namespace Epic.Training.Example.Web.Pages.Inventory
{
	[ServiceContract(Namespace="Epic.Training.Example.Web.Pages.Inventory.Services")]
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class CatalogBehavior
	{
		// To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
		// To create an operation that returns XML,
		//     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
		//     and include the following line in the operation body:
		//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

		/// <summary>Get the list of cheeses.</summary>
		/// <returns>Generic List of type Cheese.</returns>
		[OperationContract]
		public List<Cheese> GetCheeses()
		{
			List<Cheese> cheeses = null;
			try
			{ 
				CheeseList.CheeseLock.EnterReadLock();
				String filename = Global.CheeseFileName;
				CheeseList cl = XmlLoader<CheeseList>.LoadItems(filename);
				cheeses = cl.Cheeses;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message, e);
			}
			finally
			{
				CheeseList.CheeseLock.ExitReadLock();
			}
			return cheeses;
		}

		[OperationContract]
		public int AddOrReplaceCheese(Cheese newCheese)
		{
			try
			{
				CheeseList.CheeseLock.EnterWriteLock();
				String filename = Global.CheeseFileName;
				CheeseList cl = XmlLoader<CheeseList>.LoadItems(filename);
				cl.AddOrReplaceCheese(newCheese);
				XmlLoader<CheeseList>.ReplaceList(filename, cl);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message, e);
			}
			finally
			{
				CheeseList.CheeseLock.ExitWriteLock();
			}
			return newCheese.Item;
		}

		[OperationContract]
		public void MyServiceMethod() { Thread.Sleep(200); }
	}
}
