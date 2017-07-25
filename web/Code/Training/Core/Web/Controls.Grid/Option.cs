using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   
namespace Epic.Training.Core.Controls.Grid.Web
{
    /// <summary>
    /// Class to specify row or table options as a link with a tooltip.
    /// </summary>
    public class Option : ClientKeyValuePair
    {
        /// <summary>
        /// Specify the edit modes in which a particular row option is displayed
        /// </summary>
        public enum RowOptionDisplayModes
        {
            Both=0,
            IsReadOnly=1,
            NotReadOnly=2
        }

        #region public properties

        /// <summary>
        /// The displayed link text
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tooltip
        /// </summary>
		private string _toolTip;
		public string ToolTip
		{
			get { return _toolTip; }
			set { _toolTip = HttpUtility.JavaScriptStringEncode(value); }
		}

		

        [DefaultValue(RowOptionDisplayModes.Both)]
        public RowOptionDisplayModes RowOptionDisplayMode { get; set; }

        #endregion

        #region implements ClientKeyValuePair

        /// <summary>
        /// Get the key value pair of an option, which is 'Name':'ToolTip'
        /// </summary>
        /// <returns>The key value pair of an option</returns>
        public override string ToKeyValuePair()
        {
            return "'" + Name + "':{ToolTip:'" + ToolTip + "', DisplayMode:" + ((int)RowOptionDisplayMode) + "}";
        }

        #endregion
    }
}
