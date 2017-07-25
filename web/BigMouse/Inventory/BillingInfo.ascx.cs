using System;
using System.Web.UI.WebControls;

namespace Epic.Training.Example.Web.Pages.Inventory
{
	public partial class BillingInfo : System.Web.UI.UserControl
	{
		public enum CardType
		{
			visa,
			ms,
			amx
		}

		public CardType DefaultCard { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Page_Init(object sender, EventArgs e)
		{
			foreach (ListItem li in lstType.Items)
			{
				if(li.Value == DefaultCard.ToString())
				{
					li.Selected = true;
					break;
				}
			}
		}
	}
}