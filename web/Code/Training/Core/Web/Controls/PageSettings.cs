using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

// Embedd JavaScript as resource in the assembly
namespace Epic.Training.Core.Controls.Web
{
	/// <summary>
	/// Use to add server and client controls from an ASP.NET page or Web Control to this.getEl / this.getCtl
	/// in the ControlHandler class.  (In Hyperspace Web, the control manager will be replaced by ViewSettings).
	/// </summary>
	[ParseChildren(typeof(ControlEntry), ChildrenAsProperties = true)]
    public class PageSettings : ScriptControl
    {
        #region private, static and constant members

        /// <summary>
        /// Path to the embedded JavaScript file. Should be set to the Assembly name, followed by the JavaScript filename.
        /// </summary>
        //public const string EmbeddedResourcePath = "Epic.Training.Core.Controls.Web.PageBehavior.js";

        /// <summary>
        /// The name of the client namespace.  
        /// </summary>
        public static readonly string NamespaceName = typeof(PageSettings).Namespace;

        /// <summary>
        /// The name of the client class of this script control.  
        /// </summary>
        public static readonly string ClientClassName = NamespaceName + ".PageBehavior";

        /// <summary>
        /// Class property storing the collection of client control Ids on the client.
        /// </summary>
        private static readonly string ClientControlIds = ClientClassName + "." + "ClientControlIds";

        /// <summary>
        /// Class property storing the collection of server control Ids on the client.
        /// </summary>
        internal static readonly string ServerControlIds = ClientClassName + "." + "ServerControlIds";

        /// <summary>
        /// Class property storing the custom-object registry.
        /// </summary>
        internal static readonly string Objects = ClientClassName + "." + "__objects";

        private Control _rootControl;

        #endregion

        #region public members

        /// <summary>
        /// The Constructor function to call when creating the page manager
        /// </summary>
        [Category("Behavior"), DefaultValue("")]
        public string ClientClass { get; set; }

        /// <summary>
        /// The path to the client's script file
        /// </summary>
        [Category("Behavior"), DefaultValue(""), UrlProperty("*.js")]
        public string ClientScriptPath { get; set; }

        /// <summary>
        /// A list of controls to include in this.getEl and this.Ctl
        /// </summary>
        [Category("Misc"), PersistenceMode(PersistenceMode.InnerProperty), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Controls available in this.getEl"), NotifyParentProperty(true)]
        public List<ControlEntry> ManagedControls { get; private set; }

        #endregion

        #region constructors

        public PageSettings()
            : base()
        {
            ManagedControls = new List<ControlEntry>();
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            // Get all server controls on the page
            Dictionary<string, Control> controls = new Dictionary<string, Control>();

            // Get the nearest INamingContainer
            _rootControl = GetRootControl(this);

            // Copy the relevent controls
            Control[] pageControls = new Control[_rootControl.Controls.Count];
            _rootControl.Controls.CopyTo(pageControls, 0);

            List<Control> allControls = new List<Control>(pageControls);
            for (int cIdx = 0; cIdx < allControls.Count; cIdx++)
            {
                if (allControls[cIdx] is INamingContainer)
                {  // Do not dig down into naming containers
                    continue;
                }
                foreach (Control c in allControls[cIdx].Controls)
                {
                    allControls.Add(c);
                }
            }

            // Keep all controls that have an ID
            foreach (Control c in allControls)
            {
                if (!String.IsNullOrEmpty(c.ID))
                {
                    controls.Add(c.ID, c);
                }
            }

            //Next two lines output: <script type="text/javascript">
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);

            //Initialize the client and server element containers within the INamingContainer
            writer.WriteLine(ClientControlIds + "." + _rootControl.ClientID + "=[];");
            writer.WriteLine(ServerControlIds + "." + _rootControl.ClientID + "={};");

            foreach (ControlEntry c in ManagedControls)
            {
                if (controls.ContainsKey(c.ControlID))
                {  // If a server element with the same ID exists, then we have found a match.  Cache the client ID here.
                    Control ctrl = controls[c.ControlID];
                    writer.WriteLine(GetServerIdManagementClientCode(ctrl, _rootControl));
                }
                else
                { // If no server control exists, it must be a client element.  In this case the ID won't change
                    writer.WriteLine(GetClientIdManagementClientCode(_rootControl,c.ControlID));
                }
            }
            writer.RenderEndTag(); //Outputs: </script>

            base.Render(writer);
        }

        #region public helper functions

        /// <summary>
        /// Returns the client code used to add a server control to the managed server IDs collection
        /// </summary>
        /// <param name="rootControl">Nearest INamingContainer parent of the control</param>
        /// <param name="control">The control to manage</param>
        /// <returns>A client-code string (in JavaScript)</returns>
        public static string GetServerIdManagementClientCode(Control control, Control rootControl=null)
        {
            string clientCode = String.Empty;

            if (rootControl == null && control != null)
            {
                rootControl = GetRootControl(control);
            }

            if (rootControl != null && control != null)
            {  // If there is a PageSettings control on the page, add this control to the managed controls automatically
                clientCode = "if(" + ServerControlIds + "." + rootControl.ClientID + "){\n\t";
                clientCode += ServerControlIds + "." + rootControl.ClientID + "." + control.ID + "='" + control.ClientID + "';\n";
                clientCode += "}\n";
            }
            return clientCode;
        }

        /// <summary>
        /// Returns the client code used to add a client control to the managed client IDs collection
        /// </summary>
        /// <param name="rootControl">Nearest INamingContainer parent of the control</param>
        /// <param name="clientId">Client Id of the control to manage</param>
        /// <returns>A client-code string (in JavaScript)</returns>
        public static string GetClientIdManagementClientCode(Control rootControl, string clientId)
        {
            string clientCode = String.Empty;
            if (rootControl != null && !String.IsNullOrEmpty(clientId))
            {
                clientCode = ClientControlIds + "." + rootControl.ClientID + ".push('" + clientId + "');";
            }
            return clientCode;
        }

        /// <summary>
        /// Traverses the Server-side DOM until a control implementing INamingContainer is found
        /// </summary>
        /// <param name="childControl">Control to start search from</param>
        /// <returns>The root control</returns>
        public static Control GetRootControl(Control childControl)
        {
            Control rootControl = childControl.Parent;

            // Find the first parent naming container
            while (!(rootControl is INamingContainer))
            {
                rootControl = rootControl.Parent;
            }
            return rootControl;
        }

        #endregion

        #region implements ScriptControl

        /// <summary>
        /// Return the descriptors used to instantiate the control manager's client class
        /// </summary>
        /// <returns>Scriptescriptor used to generate client code</returns>
        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            if (!String.IsNullOrEmpty(ClientClass))
            {
                ScriptDescriptor s = new ManagedDescriptor(ClientClass.Trim(), _rootControl, ManagedDescriptorType.Component);
                yield return s;
            }
        }

        /// <summary>
        /// Return the client scripts (JavaScript Files) for the control manager and the
        /// page/control manager class inheriting from the Script manager.
        /// </summary>
        /// <returns>References to client scripts</returns>
        protected override IEnumerable<ScriptReference> GetScriptReferences()
        {
            // Return the control-manager's scripts
            yield return new ScriptReference("~/Assets/Training/Core/Scripts/PageBehavior.js");

            // Return custom client script
            if (!String.IsNullOrEmpty(ClientScriptPath))
            {
                yield return new ScriptReference(ResolveUrl(ClientScriptPath));
            }
        }

        #endregion
    }
}
