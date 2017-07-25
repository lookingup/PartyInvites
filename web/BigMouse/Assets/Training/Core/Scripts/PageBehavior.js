/// <reference name="MicrosoftAjax.js" />
//***************************
//  Copyright (c) 2009-2011 Epic Systems Corporation
/*global Type, Epic, $get */
//***************************
Type.registerNamespace("Epic.Training.Core.Controls.Web");

Epic.Training.Core.Controls.Web.PageBehavior = function Core$PageBehavior(clientId) {
    ///<summary>A base class for pages that must wire client events to server elements and controls.  Elements differ from controls in that
    /// the corresponding JavaScript class is built into the browser, while controls use custom, programmer-defined JavaScript class inheriting 
    /// from Sys.UI.Control</summary>
    /// <param name="clientId" type="String">The client ID of the INamingContainer corresponding to the page</param>
    ///<field type="String" name="__clientId">(private) The client ID of the page used to access managed elements and controls.</field>
    ///<field type="Object" name="__elements">(private) A collection of managed elements on the page, keyed by their server IDs.
    ///Access using the "getEl" method.</field>
    ///<field type="Object" name="__controls">(private) A collection of managed objects on the page, keyed by their server IDs.
    ///Access using the "getCtl" method.</field>

    this.__clientId = clientId;
    this.initialize();

    // Overridable methods
    this.onDataPreload(); //before loading data, after objects initialized
    this.onLoad(); //load data (e.g., asynchronously call web services)
    this.onLoaded(); //connect to client events while data is loading
};


// Array of client control IDs whose JavaScript elements will be  
// stored locally in this.__elements 
Epic.Training.Core.Controls.Web.PageBehavior.ClientControlIds = Epic.Training.Core.Controls.Web.PageBehavior.ClientControlIds || [];

// Key/Value pair of server control IDs and the corresponding element clientIDs. 
// The JavaScript element objects will be stored in this.__elements
Epic.Training.Core.Controls.Web.PageBehavior.ServerControlIds = Epic.Training.Core.Controls.Web.PageBehavior.ServerControlIds || {};

// Key/Value pair of clientIDs and the corresponding JavaScript object.
// Relevent entries will be stored in this.__controls
Epic.Training.Core.Controls.Web.PageBehavior.__objects = Epic.Training.Core.Controls.Web.PageBehavior.__objects || {};

Epic.Training.Core.Controls.Web.PageBehavior.prototype =
{
    //#region fields
    __clientId: null, // Client ID used to access managed elements and controls
    __elements: null, // A list of element objects
    __controls: null,  // A list of control objects
    //#endregion

    initialize: function Core$PageBehavior$initialize() {
        ///<summary> Retreive client and server element objects from their ids and place them in
        ///this.__elements.  Also populates this.__controls with control objects</summary>
        var id, clientId, clientElements, serverElements, objectRegistry, elements, controls, idx;

        clientElements = Epic.Training.Core.Controls.Web.PageBehavior.ClientControlIds[this.__clientId];
        serverElements = Epic.Training.Core.Controls.Web.PageBehavior.ServerControlIds[this.__clientId];
        objectRegistry = Epic.Training.Core.Controls.Web.PageBehavior.__objects;

        this.__elements = {};
        this.__controls = {};
        elements = this.__elements;
        controls = this.__controls;

        // Retrieve objects for client elements
        if (clientElements) {
            for (idx = 0; idx < clientElements.length; idx++) {
                id = clientElements[idx];
                elements[id] = $get(id);
            }
        }

        // Retrieve objects for server elements
        if (serverElements) {
            for (id in serverElements) {
                if (serverElements.hasOwnProperty(id)) {
                    clientId = serverElements[id];
                    if (objectRegistry[clientId]) {
                        controls[id] = objectRegistry[clientId];
                    }
                    else {
                        elements[id] = $get(clientId);
                    }
                }
            }
        }

        // Clear elements that have been added
        delete Epic.Training.Core.Controls.Web.PageBehavior.ClientControlIds[this.__clientId];
        delete Epic.Training.Core.Controls.Web.PageBehavior.ServerControlIds[this.__clientId];
    },

    getEl: function Core$PageBehavior$getEl(id) {
        ///<summary>Return the managed element with the given id</summary>
        ///<param name="id" type="String">ID of the dom element</param>
        ///<returns type="HTMLElement">the corresponding element</returns>
        if (this.__elements[id] !== undefined) {
        	return this.__elements[id];
        }
        else {
            throw new Error(String.format("An element with id '{0}' does not exist in the managed controls.\n\n  Did you forget to add a ControlEntry to the bottom of your aspx/ascx?", id));
        }
    },

    getCtl: function Core$PageBehavior$getCtl(id) {
        ///<summary>Return the managed control with the given id</summary>
        ///<param name="id" type="String">ID of the client control</param>
        ///<returns type="Sys.UI.Control">the corresponding control</returns>
        if (this.__controls[id] !== undefined) {
            return this.__controls[id];
        }
        else {
            throw new Error(String.format("A control with id '{0}' does not exist in the managed controls.\n\n  Did you forget to add a ControlEntry to the bottom of your aspx/ascx?", id));
        }
    },

    onDataPreload: function Core$PageBehavior$onDataPreload() {
        ///<summary>Called by the constructor before "onLoad".  
        /// Do work here before initiating asynchronous requests to the server.</summary>
    },

    onLoad: function Core$PageBehavior$onLoad() {
        ///<summary>Called by the constructor after "onDataPreload" and before "onLoaded".  
        /// Initiate asynchronous requests here.</summary>    
    },

    onLoaded: function Core$PageBehavior$onLoaded() {
        ///<summary>Called by the constructor after "onLoad".  Connect events here. 
        /// Do work here that can happen while asynchronous requests are in flight.</summary>    
    }
};

// Register the class (MS AJAX lib)
Epic.Training.Core.Controls.Web.PageBehavior.registerClass("Epic.Training.Core.Controls.Web.PageBehavior");

var $$asInputEl = Epic.Training.Core.Controls.Web.PageBehavior.asInputElement = function $$asInputEl(el)
{
	///<summary>Make the given HTMLElement appear as an HTMLInputElement to JavaScript IntelliSense in Visual Studio</summary>
	///<param name="el" type="HTMLElement">The HTML element</param>
	///<returns type="HTMLInputElement"/>
	return el;
};

var $$asSelectEl = Epic.Training.Core.Controls.Web.PageBehavior.asSelectEl = function $$asSelectEl(el)
{
	///<summary>Make the given HTMLElement appear as an HTMLSelectElement to JavaScript IntelliSense in Visual Studio</summary>
	///<param name="el" type="HTMLElement">HTML element</param>
	///<returns type="HTMLSelectElement"/>
	return el;
};

var $$asTextAreaEl = Epic.Training.Core.Controls.Web.PageBehavior.asTextAreaEl = function $$asTextAreaEl(el)
{
	///<summary>Make the given HTMLElement appear as an HTMLTextAreaElement to JavaScript IntelliSense in Visual Studio</summary>
	///<param name="el" type="HTMLElement">HTML element</param>
	///<returns type="HTMLTextAreaElement"/>
	return el;
};
//#endregion