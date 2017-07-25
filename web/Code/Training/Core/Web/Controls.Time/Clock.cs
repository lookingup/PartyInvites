using Epic.Training.Core.Controls.Web;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Epic.Training.Core.Controls.Time.Web
{
	public class Clock : ScriptControl, INamingContainer
	{
		private HtmlGenericControl _hours;
		private HtmlGenericControl _minutes;
		private HtmlGenericControl _seconds;
		private HtmlGenericControl _amPm;
		public bool IsTwentyFourHour { get; set; }
		protected override void OnInit(EventArgs e)
		{
			if(IsTwentyFourHour)
			{
				_hours.InnerText = "24";
				_amPm.InnerText = String.Empty;
			}
		}

		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			yield return new ManagedDescriptor("Epic.Training.Core.Controls.Time.Web.Clock", this, ManagedDescriptorType.Control,
				"'" + this._hours.ClientID + "'", "'" + this._minutes.ClientID + "'",
				"'" + this._seconds.ClientID + "'", "'" + this._amPm.ClientID + "'",
				this.IsTwentyFourHour.ToString().ToLower());
		}

		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			yield return new ScriptReference("~/Assets/Training/Core/Scripts/PageBehavior.js");
			yield return new ScriptReference("~/Assets/Training/Core/Scripts/Clock.js");
		}

		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		public Clock()
		{
			_hours = new HtmlGenericControl("span")
			{
				ID = "spanHours",
				InnerText = "12"
			};
			_minutes = new HtmlGenericControl("span")
			{
				ID = "spanMinutes",
				InnerText = "00"
			};
			_seconds = new HtmlGenericControl("span")
			{
				ID = "spanSeconds",
				InnerText = "00"
			};
			_amPm = new HtmlGenericControl("span")
			{
				ID = "spanAmPm",
				InnerText = "AM"
			};
			Controls.Add(_hours);
			Controls.Add(new LiteralControl(":"));
			Controls.Add(_minutes);
			Controls.Add(new LiteralControl(":"));
			Controls.Add(_seconds);
			Controls.Add(new LiteralControl(" "));
			Controls.Add(_amPm);
		}
	}
}
