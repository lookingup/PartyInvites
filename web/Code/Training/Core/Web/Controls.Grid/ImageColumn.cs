using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   
namespace Epic.Training.Core.Controls.Grid.Web
{
    /// <summary>
    /// Use in place of Column to indicate the BoundProperty is an ImageURL.  
    /// Additionally, a property for alternate text is provided
    /// </summary>
    public class ImageColumn : Column
    {
        #region public properties

        /// <summary>
        /// Text to read for screenreaders, or to display when the image is 
        /// not available.
        /// </summary>
        public string AlternateText { get; set; }

        #endregion

        #region hidden methods from Column

        /// <summary>
        /// Get the key-value pair of this column definition.
        /// When accessed as an ImageColumn, the key value pair is 
        /// 'BoundProperty':'AlternateText'.  NEW is used to preserve
        /// the key-value representation when accessed as a Column. 
        /// </summary>
        /// <returns>String representation of key-value pair</returns>
        public new string ToKeyValuePair()
        {
            return "'" + BoundProperty + "':'" + AlternateText + "'";
        }

        #endregion
    }
}
