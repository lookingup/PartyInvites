using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Epic.Training.Core.Controls.Web
{
    /// <summary>
    /// The types of managed controls
    /// </summary>
    public enum ManagedDescriptorType {
        /// <summary>
        /// Without a UI component
        /// </summary>
        Component,

        /// <summary>
        /// With a UI component
        /// </summary>
        Control
    }

    public class ManagedDescriptor : ScriptDescriptor
    {
        protected string _constructor;
        protected string _clientId;
        protected List<string> _arguments = new List<string>();


        /// <summary>
        /// The server script control being created on the client
        /// </summary>
        private Control ServerControl { get; set; }

        /// <summary>
        /// Construct the client control and add it to this.getCtl("ServerId") automatically
        /// </summary>
        /// <param name="constructor">Name of the client class (including the full namespace)</param>
        /// <param name="serverControl">Reference to the server contorl</param>
        /// <param name="args">Additional arguments to pass to the client constructor</param>
        public ManagedDescriptor(string constructor, Control serverControl, ManagedDescriptorType type, params string[] args)
        {
            _constructor = constructor;
            _clientId = serverControl.ClientID;
            _arguments.Add("'" + _clientId + "'");
            _arguments.AddRange(args);
            ServerControl = serverControl;
            if (type == ManagedDescriptorType.Control)
            {  // If the client object inherits from Sys.UI.Control, then the client object is needed to attach the object to the DOM
                this._arguments.Insert(0, "$get('" + _clientId + "')");
            }
        }

        /// <summary>
        /// Return the client code used to create the component
        /// </summary>
        /// <returns>Client code as a string</returns>
        protected override string GetScript()
        {
            // Form the code to construct the client object
            string constructionCode = PageSettings.Objects + "['" + _clientId + "'] = new " + _constructor + "(";
            string delimiter = String.Empty; // Delimiter empty for first argument

            foreach (string arg in _arguments)
            {
                constructionCode += delimiter + arg;
                delimiter = ", ";
            }

            constructionCode += ");";

            // Automatically add this control to the managed controls if ServerId and RootClientId are both provided
            string autoManageCode = String.Empty;
            if (ServerControl != null)
            {
                autoManageCode += PageSettings.GetServerIdManagementClientCode(ServerControl);
            }

            // Form the code to add the 
            return constructionCode + "\n" + autoManageCode;
        }

        /// <summary>
        /// String representation of the managed descriptor
        /// </summary>
        /// <returns>Client code</returns>
        public override string ToString()
        {
            return GetScript();
        }
    }
}
