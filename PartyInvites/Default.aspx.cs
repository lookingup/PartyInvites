using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartyInvites
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(IsPostBack)
			{
				GuestResponse rsvp = new GuestResponse();
				if(TryUpdateModel(rsvp, new FormValueProvider(ModelBindingExecutionContext)))
				{
					ResponseRepository.GetRepository().AddResponse(rsvp);
					if(rsvp.WillAttend.HasValue && rsvp.WillAttend.Value)
					{
						Response.Redirect("seeyouthere.html");
					}
					else if(rsvp.WillAttend.HasValue && rsvp.WillAttend.Value == false)
					{
						Response.Redirect("sorryyoucantcome.html");
					}
					// If default option is chosen, user is brought back to original form
					// and the form is reset.
				}
			}

		}
	}
}