using System;
using System.Collections.Generic;

namespace Epic.Training.Example.Web.Pages.Inventory
{
	public partial class Details : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int item;
			if (Int32.TryParse(Request.QueryString["item"], out item))
			{
				Cheese matchingCheese = null;
				CatalogBehavior behavior = new CatalogBehavior();
				List<Cheese> cheeseList = behavior.GetCheeses();
				foreach (Cheese cheese in cheeseList)
				{
					if (cheese.Item == item)
					{
						matchingCheese = cheese;
						break;
					}
				}
				if (matchingCheese != null)
				{
					imgFull.ImageUrl = matchingCheese.ImagePathLarge;
					txtDescription.Text = matchingCheese.Description;
					txtItem.Text = matchingCheese.Item.ToString();
					txtName.Text = matchingCheese.Name;
					txtPrice.Text = matchingCheese.Price.ToString();
					txtWeight.Text = matchingCheese.Weight.ToString();
				}
				else
				{
					Response.Redirect("~/Default.aspx");
				}
			}
			else
			{
				Response.Redirect("~/Default.aspx");
			}
		}
	}
}