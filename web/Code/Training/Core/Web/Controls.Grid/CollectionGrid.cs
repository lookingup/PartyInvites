using System;
using System.Collections.Generic;
using System.Web.UI;
using Epic.Training.Core.Controls.Web;
using System.ComponentModel;
using Epic.Training.Core.Controls.Grid.Web;
//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   

[assembly: WebResource(CollectionGrid.EmbeddedResourcePath, "text/javascript")]
namespace Epic.Training.Core.Controls.Grid.Web
{
	/// <summary>
	/// Renders as a table backed by a collection of JavaScript objects
	/// </summary>
	[ParseChildren(true)]
    [ToolboxData("<{0}:CollectionGrid runat=\"server\"></{0}:CollectionGrid>")]
    public class CollectionGrid : ScriptControl
    {

        #region constants
        /// <summary>
        /// The namespace and assembly name of the server and client classes
        /// </summary>
        public const string NamespaceName = "Epic.Training.Core.Controls.Grid.Web";

        /// <summary>
        /// The JavaScript class constructor used to create an instance of a CollectionGrid
        /// </summary>        
        public const string ClientClass = NamespaceName + "." + "CollectionGrid";

        /// <summary>
        /// Path to the embedded JavaScript file
        /// </summary>
        public const string EmbeddedResourcePath = ClientClass + ".js";

        #endregion

        #region properties

        /// <summary>
        /// Defines each column of the table and the property that they bind to
        /// </summary>
        [Category("Misc"), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Column definitions"), NotifyParentProperty(true)]
        public List<Column> ColumnDefinitions { get; private set; }

        private List<ImageColumn> ImageColumnDefinitions { get; set; }
        private List<EditColumn> EditColumnDefinitions { get; set; }


        /// <summary>
        /// Defines options that render as links in the last column of the table.  These links
        /// can be bound to using addRowOptionHandler(delegate)
        /// </summary>
        [Category("Misc"), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Row Options"), NotifyParentProperty(true)]
        public List<Option> RowOptions { get; private set; }

        /// <summary>
        /// A list of options specific to the table as a whole, rather than an individual row.  These links
        /// can be bound to using addTableOptionHandler(delegate)
        /// </summary>
        [Category("Misc"), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Table Options"), NotifyParentProperty(true)]
        public List<Option> TableOptions { get; private set; }

        /// <summary>
        /// The outermost tag used to render this script control
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

        /// <summary>
        /// True to show table headers for each column, false to hide them.
        /// </summary>
        [DefaultValue(true)]
        public bool ShowHeaders { get; set; }

        /// <summary>
        /// True to show a column at the beginning of the table with a radio button for each row.
        /// </summary>
        [DefaultValue(false)]
        public bool ShowSelectColumn { get; set; }

        /// <summary>
        /// When true, rows are initially rendered in read-only mode (without the edit control).  When false, the
        /// control is added to each row of all edit columns.
        /// </summary>
        [DefaultValue(false)]
        public bool ReadOnly { get; set; }

        #endregion

        #region constructors

        public CollectionGrid()
            : base()
        {
            ColumnDefinitions = new List<Column>();
            RowOptions = new List<Option>();
            TableOptions = new List<Option>();
            ImageColumnDefinitions = new List<ImageColumn>();
            EditColumnDefinitions = new List<EditColumn>();
        }


        #endregion

        #region instance methods

        /// <summary>
        /// Called when initializing the grid.  For the collection grid, this method places
        /// collumns of different types in the corresponding collection.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnInit(EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ID))
            {
                throw new Exception("A collection grid must have an ID");
            }
            foreach (Column c in ColumnDefinitions)
            {
                if (c is EditColumn)
                {
                    EditColumn ec = (EditColumn)c;
                    EditColumnDefinitions.Add(ec);

                    //Edit columns that should display as images when in read-only mode are 
                    //added to the ImageColumnDefinitions list.
                    if (ec.DisplayType == EditColumn.DisplayTypes.Image)
                    {
                        ImageColumnDefinitions.Add(ec);
                    }
                }
                else if (c is ImageColumn)
                { // Add image columns that are not edit columns.
                    ImageColumnDefinitions.Add((ImageColumn)c);
                }
            }
        }

		/// <summary>
        /// Render the table and include a hidden input of type text to prevent post back when enter is 
        /// pressed within a textbox.  This only occurs when the form contains exactly 1 textbox.
        /// </summary>
        /// <param name="writer">Object used to write to the page.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            // The following line prevents the "return causes postback" problem when there is a form with
            // only one text input.
            writer.WriteLine("<input type=\"text\" style=\"display:none\" />");
        }
		
        #endregion

        #region class methods

        /// <summary>
        /// Create a JSON object (string of the form {'key1':'val1', ... ,'keyN':'valN'})
        /// by extracting each key-value pair of a given type from an enumerable object
        /// </summary>
        /// <typeparam name="TListType">The type returned by the enumerable object.</typeparam>
        /// <param name="fromList">The enumerable object to extract from</param>
        /// <returns>A JSON serialization with key-value pairs for each extracted item</returns>
        private static string GetClientObject<TListType>(IEnumerable<TListType> fromList)
            where TListType : ClientKeyValuePair
        {
            // The list is empty, no need to continue
            if (fromList == null)
            {
                return "null";
            }

            string keyValuePairs = "";
            string delimiter = "";
            foreach (TListType item in fromList)
            {
                keyValuePairs += delimiter + ClientKeyValuePair.GetTypeSpecificKeyValuePair<TListType>(item);
                delimiter = ",";
            }

            if (String.IsNullOrEmpty(keyValuePairs))
            {
                return "null"; // Return so JavaScript sees the null value
            }
            return "{" + keyValuePairs + "}"; //JSON notation for a literal object {"key":"val", ...}
        }

        #endregion

        #region implement ScriptControl

        /// <summary>
        /// Returns code to construct the client-side version of this server object
        /// </summary>
        /// <returns>JavaScript code descriptor</returns>
        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            // Prepare constructor arguments for JavaScript
            string columnDefinitionsObject = GetClientObject<Column>(ColumnDefinitions);
            string tableOptionsObject = GetClientObject<Option>(TableOptions);
            string rowOptionsObject = GetClientObject<Option>(RowOptions);
            string showHeaders = ShowHeaders.ToString().ToLower();
            string showSelectColumn = ShowSelectColumn.ToString().ToLower();
            string readOnly = ReadOnly.ToString().ToLower();
            string imageColumnsObject = GetClientObject<ImageColumn>(ImageColumnDefinitions);
            string editColumnsObject = GetClientObject<EditColumn>(EditColumnDefinitions);

            // Create the descriptor
            ManagedDescriptor constructorCall = new ManagedDescriptor(
                ClientClass,
                this,
                ManagedDescriptorType.Control,
                columnDefinitionsObject,
                tableOptionsObject,
                rowOptionsObject,
                showHeaders,
                showSelectColumn,
                readOnly,
                imageColumnsObject,
                editColumnsObject
            );

            // Return code to construct collection table on client
            yield return constructorCall;
        }

        /// <summary>
        /// Return references to the client sripts for the ControlManager and CollectionGrid
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<ScriptReference> GetScriptReferences()
        {
			// Get PageBehavior.js
			yield return new ScriptReference("~/Assets/Training/Core/Scripts/PageBehavior.js");

			// Get CollectionGrid.js
			yield return new ScriptReference("~/Assets/Training/Core/Scripts/CollectionGrid.js");
		}

        #endregion

    }
}