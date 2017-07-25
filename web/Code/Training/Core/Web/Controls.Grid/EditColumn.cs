using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   
namespace Epic.Training.Core.Controls.Grid.Web
{
    /// <summary>
    /// A column of the CollectionGrid that can be edited.  Inherits from Image column because the EditColumn can render as
    /// either text or an image, depending on the DisplayType property.
    /// </summary>
    public class EditColumn : ImageColumn
    {
        /// <summary>
        /// Available control types for edit columns of the CollectionGrid
        /// </summary>
        public enum ControlTypes
        {
            /// <summary>
            /// Renders as a checkbox in edit mode
            /// </summary>
            Checkbox=0,
            /// <summary>
            /// Renders as a select control in edit mode
            /// </summary>
            Select=1,
            /// <summary>
            /// Renders as a textbox in edit mode
            /// </summary>
            Text=2,
            /// <summary>
            /// Renders as a textarea control in edit mode
            /// </summary>
            TextArea=3,
        }

        /// <summary>
        /// Available ways the data within this column can be displayed.
        /// </summary>
        public enum DisplayTypes
        {
            /// <summary>
            /// Renders as an image in ReadOnly mode
            /// </summary>
            Image,
            /// <summary>
            /// Renders as text in ReadOnly mode
            /// </summary>
            Text
        }

        /// <summary>
        /// The default constructor for an edit column.  Defaults to Text for both display and editing.
        /// </summary>
        public EditColumn()
        {
            SelectOptions = new List<SelectOption>();
        }

        /// <summary>
        /// Specify the type of control used to edit the bound property of this column
        /// </summary>
        [DefaultValue(ControlTypes.Text)]
        public ControlTypes Type { get; set; }

        /// <summary>
        /// Specify how an edit control is displayed when in read-only mode
        /// </summary>
        [DefaultValue(DisplayTypes.Text)]
        public DisplayTypes DisplayType { get; set; }

        /// <summary>
        /// Number of rows for a Type of TextArea
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Number of columns for a Type of TextArea
        /// </summary>
        public int Cols { get; set; }

        /// <summary>
        /// Specify options available to choose from when the Type is set to ControlType.Select
        /// </summary>
        [Category("Misc"), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Select Options"), NotifyParentProperty(true)]
        public List<SelectOption> SelectOptions { get; private set; }
        
        /// <summary>
        /// Return the key-value pair of this edit column.  The value is extra inforamation required
        /// to render the control.  For example, a set of options for a Select control, or rows and cols for
        /// a TextArea control
        /// </summary>
        /// <returns>A Key Value pair of control type/extra information</returns>
        public new string ToKeyValuePair()
        {
            string keyValuePair="'" + BoundProperty + "':{Type:" + ((int)Type) + ", Options:{";
            string delimiter = "";
            switch(Type)
            {
                case ControlTypes.Select:
                    foreach (SelectOption option in SelectOptions)
                    {
                        keyValuePair += delimiter + option.ToKeyValuePair();
                        delimiter = ", ";
                    }
                    break;
                case ControlTypes.TextArea:
                    
                    if (Rows <= 0)
                    {
                        Rows = 5;
                    }
                    if (Cols <= 0)
                    {
                        Cols = 20;
                    }
                    keyValuePair += "Rows:" + Rows + ", Cols: " + Cols;
                    break;

                case ControlTypes.Text:
                    break;
            }

            return keyValuePair + "}}";
        }


    }
}
