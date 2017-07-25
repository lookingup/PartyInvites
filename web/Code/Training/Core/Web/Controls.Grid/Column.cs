using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   
namespace Epic.Training.Core.Controls.Grid.Web
{
    /// <summary>
    /// Represents a column definition as a key/value pair, where the 
    /// BoundProperty is the key and Header is the definition.
    /// </summary>
    public class Column : ClientKeyValuePair
    {

        #region private members

        /// <summary>
        /// Private member corresponding to Header
        /// </summary>
        private string _header;

        #endregion

        #region public properties

        /// <summary>
        /// Property bound to this column of the Collection table.
        /// </summary>
        [DefaultValue("")]
        public string BoundProperty { get; set; }

        [DefaultValue("")]
        public string CssClass { get; set; }

        /// <summary>
        /// The header text for this column definition.  If Header has not been explicitly set,
        /// then BoundProperty is returned in its place.
        /// </summary>
        public string Header
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_header))
                {
                    return BoundProperty;
                }
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        #endregion

        #region implements ClientKeyValuePair

        /// <summary>
        /// Get the key-value pair representation of this column, as 'BoundProperty':'Header'.
        /// </summary>
        /// <returns>String representation of key-value pair</returns>
        public override string ToKeyValuePair()
        {
            return "'" + BoundProperty + "':{Header:'" + Header + "',CssClass:'" + CssClass + "'}";
        }

        #endregion
    }
}
