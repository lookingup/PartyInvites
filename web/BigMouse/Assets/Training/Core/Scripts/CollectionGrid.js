/// <reference name="MicrosoftAjax.js" />

//***************************
//  Copyright (c) 2009 Epic Systems Corporation
//
//	Name: Epic.Training.Core.Controls.Grid.Web.CollectionGrid
//  Description: Display a collection of objects in a tabular format, with support for adding
//               options to each row or the table itself.  These objects are rendered as links, 
//               to which event handlers can be attached.
//  <type name="Epic.Training.Clock" />
//  <uses name="MicrosoftAjax" />
//  Assumes:
/*global Type, Epic, alert, $addHandler, document, Sys, $get, setTimeout */
//***************************
Type.registerNamespace("Epic.Training.Core.Controls.Grid.Web");

//#region constructor
Epic.Training.Core.Controls.Grid.Web.CollectionGrid =
function Core$CollectionGrid(element, clientId, columnDefinitions, tableOptions, rowOptions, showHeaders, showSelectColumn, readOnly, imageColumns, editColumns) {
    /// <summary>Constructor for the CollectionGrid class.</summary>
    /// <param name="element" type="Sys.UI.DomElement">The XHTML table that this collection grid is connected to.</param>
    /// <param name="clientId" type="String">The unique client id of this collection Grid.  It should match the ID of the provided element</param>
    /// <param name="columnDefinitions" type="Object">Object of the form {'prop':{Header:'header text', CssClass:'className'}, ...}, where prop is the name of 
    ///  a property of objects added to this table.</param>
    /// <param name="tableOptions" type="Object">Object of the form {'Option Name':{ToolTip:'tip text'}, ...}.  'Option Name' is both the text of the resulting link and the unique 
    ///  identifier of the option used when attaching event handlers.</param>
    /// <param name="rowOptions" type="Object">Object of the form {'Option Name':{ToolTip:'tip text', DisplayMode:'IsReadOnly/NotReadOnly/Both'}, ...}.  
    ///  'Option Name' is both the text of the resulting link and the unique identifier of the option used when attaching event handlers.  DisplayMode indicates if this option is displayed
    ///  when the row is read only, not read only, or in both cases.  These options are enumerated in Epic.Training.Core.Controls.Grid.Web.CollectionGrid.RowOptionDisplayModes</param>
    /// <param name="showHeaders" type="Boolean">If true, a thead is added to element that contains one th per column definition.</param>
    /// <param name="showSelectColumn" type="Boolean">If true, an extra first column is added containing a radio button for each row.  Clicking the row will cause the corresponding
    ///  radio button to be selected</param>
    /// <param name="readOnly" type="Boolean">If true, all rows are initially rendered in read-only mode. Later, you can use set_rowReadOnly to change the read-only mode for a particular row.
    ///  Note that rows will revert to readOnly when refreshed. </param>
    /// <param name="imageColumns" type="Object">An object of the form {'prop':'Alternate text', ...} where prop is the name of a property that also appears in columnDefinitions. 
    ///  if a corresponding entry is provided here, then the column will be rendered as an image rather than text, where the property value is assumed to be a path to the image.</param>
    /// <param name="editColumns" type="Object">An object of the form 
    ///  {'prop':{Type:'Checkbox/Select/Text/TextArea', Options:{Rows:'numRows', Cols:'numCols' / 'option value':'option display text', ...}}}
    ///  where prop is the name of a porperty that must also appears in columnDefinitions.  When in edit mode, the edit column displays the property value 
    ///  of the bound object in an XHTML form element as indicated by Type.  When Type is TextArea, Rows and Cols must be provided in Options to indicate the size of the control (and maintain
    ///  XHTML compatibility).  When Type is Select, Options will be rendered to a list of option elements that the user can choose from.  When the row is readonly, an edit column will render 
    ///  as text unless there is a corresponding entry for prop in imageColumns, in which case it will render as an image.  
    ///  Type is enumerated in Epic.Training.Core.Controls.Grid.Web.CollectionGrid.EditTypes</param>
    /// <field name="__cssClassList" type="Object">(private) A collection of CSS classes used within the table.
    ///    (1) dataRow: Applied to objects added to the grid
    ///    (2) rowOptions: Applied to the every cell of the row-options column.</field> 
    /// <field name="__selectedObject" type="Object">(private) If row selectin is enabled, the selected object is cached here.</field>
    /// <field name="__id" type="String">(private) Client ID of this object</field>
    /// <field name="__showHeaders" type="Boolean">(private) Indicates that column headers are displayed</field>
    /// <field name="__showSelectColumn" type="Boolean">(private) Indicates that a slection column with radio buttons is displayed</field>
    /// <field name="__readOnly" type="Boolean">(private) Indicates that rows in this table start as read-only</field>
    /// <field name="__table" domElement="true">(private) The table element associated with CollectionGrid.</field>
    /// <field name="__columnDefs" type="Object">(private) A keyed collection of "poperty/Headers"</field>
    /// <field name="__rows" type="Array">(private) An array of objects that are displayed as rows</field>
    /// <field name="__rowElements" type="Array">(private) The element of type "row" that corresponds to the objects that row represents</field>
    /// <field name="__rowCssClasses" type="Array">(private) Array of additional CssClasses for each row</field>
    /// <field name="__rowOptionHandlers" type="Object">(private)Collection of event handlers for each row option.</field>
    /// <field name="__rowOptionTooltips" type="Object">(private)Tooltips for each row option</field>
    /// <field name="__rowOptionDisplayModes" type="Object">(private) Determines when each row option should be displayed (IsReadOnly, NotReadOnly, Both)</field>
    /// <field name="__tableOptions" type="Object">(private) A keyed collection of "link-text/link tool-tips"</field>
    /// <field name="__tableLinks" type="Array">(private) The links contianed in the footer</field>
    /// <field name="__footer" domElement="true">(private) The footer row that contains links to table options
    /// <field name="__header" domElement="true">(private) The header row</field>
    /// <field name="__imageColumns" type="Object">(private) A keyed collection of "column names / alt text"</field>
    /// <field name="__editColumns" type="Object">(private) A keyed collection of "column names / edit options"</field>
    /// <field name="__checkboxClickDelegate" type="Function">(private) Delegate used with columns bound to checkboxes.</field>
    /// <field name="__controlBlurDelegate" type="Function">(private) Delegate used when embedded controls lose focus.</field>
    Epic.Training.Core.Controls.Grid.Web.CollectionGrid.initializeBase(this, [element]);

    var headerCell, headerText, prop, colCount, option, cell, footer, javascript, a;

    // Setup instance members
    this.__id = clientId;
    this.__showHeaders = showHeaders;
    this.__showSelectColumn = showSelectColumn;
    this.__readOnly = readOnly;

    this.__columnDefs = columnDefinitions;
    this.__table = element;
    this.__table.appendChild(document.createElement("tBody"));
    this.__rows = [];
    this.__rowElements = [];
    this.__rowCssClasses = [];
    this.__imageColumns = imageColumns;
    this.__editColumns = editColumns;

    this.__checkboxClickDelegate = Function.createDelegate(this, this.__checkboxCellClicked);
    this.__controlBlurDelegate = Function.createDelegate(this, this.__controlLostFocus);

    // Setup row options
    if (rowOptions) {
        this.__rowOptionHandlers = {};
        this.__rowOptionTooltips = {};
        this.__rowOptionDisplayModes = {};

        // Collect list of options, and assign an empty handler array
        for (option in rowOptions) {  // Separate the property names from the tooltips.
            if (rowOptions.hasOwnProperty(option)) {
                this.__rowOptionHandlers[option] = [];
                this.__rowOptionTooltips[option] = (rowOptions[option] === "" ? null : rowOptions[option].ToolTip);
                this.__rowOptionDisplayModes[option] = rowOptions[option].DisplayMode;
            }
        }
    }

    //Setup table headers
    colCount = 0;
    if (this.__showHeaders) { // Create a header to display 
        this.__header = document.createElement("tr");

        if (this.__showSelectColumn) {  // If the row can be selected, pre-pend this column
            headerCell = document.createElement("th");
            headerText = document.createTextNode("Select");
            headerCell.appendChild(headerText);
            this.__header.appendChild(headerCell);
            colCount++;
        }

        for (prop in this.__columnDefs) {  // One column per property in the column definitions
            if (this.__columnDefs.hasOwnProperty(prop)) {
                headerCell = document.createElement("th");
                headerText = document.createTextNode(this.__columnDefs[prop].Header);
                headerCell.appendChild(headerText);
                this.__header.appendChild(headerCell);
                colCount++;
            }
        }

        if (rowOptions) {  // If the table has row options, append an extra column for those
            headerCell = document.createElement("th");
            headerText = document.createTextNode("Options");
            headerCell.appendChild(headerText);
            this.__header.appendChild(headerCell);
            Sys.UI.DomElement.addCssClass(headerCell, this.__cssClassList.rowOptions);
            colCount++;
        }

        this.__table.createTHead();
        this.__table.tHead.appendChild(this.__header);
    }
    else if (tableOptions) {  // Row count is still necessary if there are table options
        if (this.__showSelectColumn) {
            colCount++;
        }
        if (rowOptions) {
            colCount++;
        }
        for (prop in this.__columnDefs) {
            if (this.__columnDefs.hasOwnProperty(prop)) {
                colCount++;
            }
        }
    }

    // Setup table options

    if (tableOptions) { // Create a footer to display table options
        this.__footer = document.createElement("tr");
        footer = this.__footer;
        cell = document.createElement("td");
        cell.setAttribute("colspan", colCount);
        footer.appendChild(cell);

        this.__tableLinks = [];
        this.__tableOptions = {};

        // Collect list of options, and assign empy handler array
        for (option in tableOptions) {
            if (tableOptions.hasOwnProperty(option)) {
                cell.appendChild(document.createTextNode(" "));
                a = document.createElement("a");
                a.appendChild(document.createTextNode(option));
                javascript = "javascript";
                a.setAttribute("href", javascript + ":;");
                a.setAttribute("title", tableOptions[option].ToolTip);
                cell.appendChild(a);
                this.__tableLinks.push(a);
                this.__tableOptions[option] = a;
            }
        }
        this.__table.createTFoot();
        this.__table.tFoot.appendChild(footer);
    }
};
//#endregion

Epic.Training.Core.Controls.Grid.Web.CollectionGrid.prototype = {
    //#region fields
    __cssClassList: { dataRow: "cgData", rowOptions: "cgRowOptions" },
    __selectedObject: null,
    __id: null,
    __showHeaders: false,
    __showSelectColumn: false,
    __readOnly: false,
    __table: null,
    __columnDefs: null,
    __rows: null,
    __rowElements: null,
    __rowCssClasses: null,
    __rowOptionHandlers: null,
    __rowOptionTooltips: null,
    __rowOptionDisplayModes: null,
    __tableOptions: null,
    __tableLinks: null,
    __footer: null,
    __header: null,
    __imageColumns: null,
    __editColumns: null,
    __checkboxClickDelegate: null,
    __controlBlurDelegate: null,
    //#endregion
    //#region public properties
    get_collectionArray: function Core$CollectionGrid$get_collectionArray() {
        ///<summary>Get an array containing the objects in this table.  NOTE: Changing what objects are in the array
        ///will not change the rows in the table, but changing the actual object properties will be reflected in 
        ///the table, once the table is refreshed.</summary>
        ///<returns type="Array">A new array of objects</returns>
        var toReturn, rIdx;
        toReturn = [];
        for (rIdx = 0; rIdx < this.__rows.length; rIdx++) {
            toReturn.push(this.__rows[rIdx]);
        }
        return toReturn;
    },

    set_collectionArray: function Core$CollectionGrid$set_collectionArray(value) {
        ///<summary>Set an array containing the objects in this table</summary>
        /// <param name="value" type="Array">The collection array to set this grid to</param>
        var e, idx;
        //#if DEBUG	
        e = Function.validateParameters(arguments, [
            { name: "value", type: Array }
        ]);
        if (e) { throw e; }
        //#endif 

        if (typeof (value.length) !== "undefined") {
            this.clear();
            for (idx = 0; idx < value.length; idx++) {
                this.addRow(value[idx]);
            }
        }
        else {
            throw new Error("Value must be an array");
        }
    },

    get_selectedObject: function Core$CollectionGrid$get_selectedObject() {
        ///<summary>Get the currently selected object.</summary>	
        ///<returns type="Object">The object corresponding to the selected row</returns>
        return this.__selectedObject;
    },
    //#endregion
    //#region public methods
    set_rowReadOnly: function Core$CollectionGrid$set_rowReadOnly(rowObject, isReadOnly) {
        /// <summary>Set the readOnly mode of the row corresponding to rowObject.  If the table is NOT set to readOnly, 
        ///  then this has no effect other than refreshing the row.</summary>
        /// <param name="rowObject" type="Object">Object bound to the row for which edit mode is being toggled</param>
        /// <param name="isReadOnly" type="Boolean">True to insert controls, false to remove them</param>

        //#if DEBUG	
        var e = Function.validateParameters(arguments, [
                { name: "rowObject", type: Object },
                { name: "isReadOnly", type: Boolean }
                ]);
        if (e) { throw e; }
        //#endif 

        this.__replaceRow(rowObject, rowObject, null, !isReadOnly);
    },
    addTableOptionHandler: function Core$CollectionGrid$addTableOptionHandler(option, handler) {
        ///<summary>Add an event handler that will be called when the indicated table option is 
        ///  clicked. </summary>
        ///<param name="option" type="String">The name of the option</param>
        ///<param name="handler" type="Function">The handler (created using Function.createDelegate)
        ///  that will be called when the table option link is clicked.</param>
        var e, options, link;
        //#if DEBUG	
        e = Function.validateParameters(arguments, [
                { name: "option", type: String },
                { name: "handler", type: Function }
                ]);
        if (e) { throw e; }
        //#endif 

        options = this.__tableOptions;
        if (options) {
            link = options[option];
            if (link) {
                $addHandler(link, "click", handler);
            }
        }
    },

    addRowOptionHandler: function Core$CollectionGrid$addRowOptionHandler(option, handler) {
        ///<summary>Add an event handler that will be called when the indicated option is clicked.
        ///  The handler will only be applied to new rows as they are added, not existing rows
        ///  (unless the existing rows are refreshed).</summary>
        ///<param name="option" type="String">The name of the option</param>
        ///<param name="handler" type="Function">The handler (created using Function.createDelegate)
        ///  that will be called when the row option link is clicked.</param>

        var e, handlerList;

        //#if DEBUG	
        e = Function.validateParameters(arguments, [
                { name: "option", type: String },
                { name: "handler", type: Function }
                ]);
        if (e) { throw e; }
        //#endif 

        if (this.__rowOptionHandlers) {
            handlerList = this.__rowOptionHandlers[option];
            if (handlerList) {
                handlerList.push(handler);
            }
        }
    },

    clear: function Core$CollectionGrid$clear() {
        ///<summary>Remvoe all objects from the collectin and clear the table.</summary>	
        var tbodies, row;

        tbodies = this.__table.tBodies[0];
        for (row = tbodies.childNodes.length - 1; row >= 0; row--) {
            tbodies.deleteRow(row);
        }

        this.__rows = [];
        this.__rowElements = [];
        this.__rowCssClasses = [];
        this.__selectedObject = null;
    },

    deleteRow: function Core$CollectionGrid$deleteRow(rowObject) {
        ///<summary>Remvoe the indicated row object from the table</summary>
        ///<param name="rowObject" type="Object">The object contained in this collection table
        ///  that will be removed</param>
        ///<returns type="Boolean">true if a matching row was deleted, false otherwise.</returns>

        var e, idxToDelete, rIdx;

        //#if DEBUG	
        e = Function.validateParameters(arguments, [
            { name: "rowObject", type: Object }
            ]);
        if (e) { throw e; }
        //#endif 

        idxToDelete = -1;
        for (rIdx = 0; rIdx < this.__rows.length; rIdx++) {
            if (this.__rows[rIdx] === rowObject) {
                idxToDelete = rIdx;
                break;
            }
        }

        if (idxToDelete !== -1) {
            this.__table.tBodies[0].deleteRow(rIdx);
            this.__rows.splice(rIdx, 1);
            this.__rowElements.splice(rIdx, 1);
            this.__rowCssClasses.splice(rIdx, 1);
            return true;
        }

        return false;
    },

    addRow: function Core$CollectionGrid$addRow(rowObject, rowCssClass) {
        ///<summary>Add the indicated row object to the table</summary>
        ///<param name="rowObject" type="Object">The object to be added to this
        ///  collection table</param>
        ///<param name="rowCssClass" type="String">[Optional] The name of an additonal css class
        ///  to add to this row. The class "data" is always assigned.</param>

        var e, row;

        //#if DEBUG	
        if (typeof (rowCssClass) !== "undefined") {
            e = Function.validateParameters(arguments, [
                { name: "rowObject", type: Object },
                { name: "rowCssClass", type: String }
                ]);
        }
        else {
            e = Function.validateParameters(arguments, [
                { name: "rowObject", type: Object }
                ]);
        }
        if (e) { throw e; }
        //#endif 

        row = this.__createRow(rowObject, rowCssClass, this.__rows.length);

        this.__rows.push(rowObject);

        if (rowCssClass) {
            this.__rowCssClasses.push(rowCssClass);
        }

        this.__table.tBodies[0].appendChild(row);
        this.__rowElements.push(row);
    },

    replaceRow: function Core$CollectionGrid$replaceRow(newRowObject, oldRowObject, rowCssClass) {
        ///<summary>Replace an object already in this table with another</summary>
        ///<param name="newRowObject" type="Object">The new object that will replace the old</param>
        ///<param name="oldRowObject" type="Object">The object being replaced</param>

        //#if DEBUG	
        var e;
        if (typeof (rowCssClass) !== "undefined") {
            e = Function.validateParameters(arguments, [
                { name: "newRowObject", type: Object },
                { name: "oldRowObject", type: Object },
                { name: "rowCssClass", type: String }
                ]);
        }
        else {
            e = Function.validateParameters(arguments, [
                { name: "newRowObject", type: Object },
                { name: "oldRowObject", type: Object }
                ]);
        }
        if (e) { throw e; }
        //#endif 

        this.__replaceRow(newRowObject, oldRowObject, rowCssClass, false);
    },

    refreshRow: function Core$CollectionGrid$refreshRow(rowObject) {
    	///<summary>Refreshes an existing row</summary>
        ///<param name="rowObject" type="Object">The object to refresh</param>

        //#if DEBUG	
        var e = Function.validateParameters(arguments, [
                { name: "rowObject", type: Object }
                ]);
        if (e) { throw e; }
        //#endif 

        this.replaceRow(rowObject, rowObject);
    },

    refreshAllRows: function Core$CollectionGrid$refreshAllRows(rowObject) {
    	///<summary>Refresh all rows in the grid</summary>
        this.set_collectionArray(this.get_collectionArray());
    },

    select: function Core$CollectionGrid$select(rowObject) {
        ///<summary>Places focus on the first link or control in the table, if one exists.</summary>
        /// <param name="rowObject" type="Object">[Optional] Set focus on the first link or control in the row corresponding to this object.</param>

        var e, element, rIdx, row;

        //#if DEBUG	
        if (rowObject !== null && typeof (rowObject) !== "undefined") {
            e = Function.validateParameters(arguments, [
					{ name: "rowObject", type: Object }
					]);
            if (e) { throw e; }
        }
        //#endif 

        element = null;
        if (rowObject) {
            for (rIdx = 0; rIdx < this.__rows.length; rIdx++) {
                row = this.__rows[rIdx];
                if (rowObject === row) {
                    element = this.__rowElements[rIdx];
                }
            }
            element = this.__searchForElementWithSelectOrFocusMethod(element);
        }
        else {
            for (rIdx = 0; rIdx < this.__rows.length; rIdx++) {
                element = this.__rowElements[rIdx];
                element = this.__searchForElementWithSelectOrFocusMethod(element);
                if (element !== null) {
                    break;
                }
            }
            if (!element) {
                if (this.__footer) {
                    element = this.__searchForElementWithSelectOrFocusMethod(this.__footer);
                }
            }
        }

        if (element) {
            if (typeof (element.select) !== "undefined") {
                element.select();
                element.select(); // IE has a bug where select must be called twice in certian situations
            }
            else if (typeof (element.focus) !== "undefined") {
                element.focus();
            }
        }

    },
    //#endregion
    //#region private methods
    __searchForElementWithSelectOrFocusMethod: function Core$CollectionGrid$__searchForElementWithSelectOrFocusMethod(element)
    {
        /// <summary>(private) Recursively searches (DFS) for an element that can gain focus (A, INPUT, TEXTAREA or SELECT).</summary>

        var toReturn, idx, child;

        toReturn = null;
        for (idx = 0; idx < element.childNodes.length; idx++) {
            child = element.childNodes[idx];
            if (child.nodeName === "A" || child.nodeName === "INPUT" || child.nodeName === "TEXTAREA" || child.nodeName === "SELECT") {
                toReturn = child;
                break;
            }
            else if (child.childNodes && child.childNodes.length > 0) {
                toReturn = this.__searchForElementWithSelectOrFocusMethod(child);
                if (toReturn !== null) {
                    break;
                }
            }
        }
        return toReturn;
    },

    __replaceRow: function Core$CollectionGrid$__replaceRow(newRowObject, oldRowObject, rowCssClass, forceEdit) {
        ///<summary>(private) Replace an object already in this table with another</summary>
        ///<param name="newRowObject" type="Object">The new object that will replace the old</param>
        ///<param name="oldRowObject" type="Object">The object being replaced</param>
        /// <param name="forceEdit" type="Boolean">If true, render the new row in edit mode.  If false, render it according to the table read-only mode</param>

        var toReplace, rIdx, row, cIdx;

        toReplace = null;
        for (rIdx = 0; rIdx < this.__rows.length; rIdx++) {
            toReplace = this.__rows[rIdx];
            if (toReplace === oldRowObject) {
                break;
            }
        }

        if (toReplace !== oldRowObject) {
            // oldRowObject is not in the table
            return;
        }

        if (rowCssClass) {
            this.__rowCssClasses[rIdx] = rowCssClass;
        }




        row = this.__createRow(newRowObject, this.__rowCssClasses[rIdx], rIdx, forceEdit);
        this.__table.tBodies[0].insertBefore(row, this.__rowElements[rIdx]);
        this.__table.tBodies[0].deleteRow(rIdx + 1);
        this.__rows[rIdx] = newRowObject;
        this.__rowElements[rIdx] = row;


        // Handle the IE9 bug where checkboxes don't display correctly
        if (this.__ceckboxBugFixes) {
            for (cIdx = 0; cIdx < this.__ceckboxBugFixes.length; cIdx++) {
                this.__ceckboxBugFixes[cIdx]();
            }
            this.__checkboxBugFixes = null;
        }

    },

    __updateCheckbox: function Core$CollectionGrid$_updateCheckbox(args) {
        /// <summary>There is a bug in IE9 where the checked state of a checkbox resets 
        /// after it is added to the DOM.  This method is used to fix that problem.</summary>
        /// <param name="args" type="Object">Contains two parameters:  
        /// id: The client id of the checkbox
        /// value: A boolean indicating if the checkbox should be checked</param>
        $get(args.id).checked = args.value;
    },

    __createRow: function Core$CollectionGrid$__createRow(rowObject, rowCssClass, pos, forceEdit) {
        ///<summary>(private) Create a row element to display the given row object.  The given CSS class
        ///  is added to the row if one is provided.  The css class "data" is assigned to every row.</summary>
        ///<param name="rowObject" type="Object">The object that will be displayed in the new row</param>
        ///<param name="rowCssClass" type="String">[Optional] An additional CSS class for the row.  The class 
        /// "data" is assigned by default.</param>
        ///<param name="pos" type="Number">The position of the row in the table</param>
        /// <param name="forceEdit" type="Boolean">Enable edit controls even if the table is set to read only.</param>

        var cell, editMode, row, radio, del, cal, cellContent, imageColumns, editColumns, clickDelegate, changeDelegate,
            args, option, prop, editColumn, editType, value, a, handlers, idx, javascript, callback, checkboxBugFixes;

        editMode = !(this.__readOnly && !forceEdit);
        row = document.createElement("tr");

        Sys.UI.DomElement.addCssClass(row, this.__cssClassList.dataRow);

        if (rowCssClass) {
            Sys.UI.DomElement.addCssClass(row, rowCssClass);
        }

        if (this.__showSelectColumn) {
            cell = document.createElement("td");
            radio = Epic.Training.Core.Controls.Grid.Web.CollectionGrid.createRadio(this.__id, pos);
            cell.appendChild(radio);
            row.appendChild(cell);
            del = Function.createDelegate(this, this.__selectRow);
            cal = Function.createCallback(del, [radio, rowObject]);
            $addHandler(row, "click", cal);
        }


        // Create cells
        imageColumns = this.__imageColumns || {};
        editColumns = this.__editColumns || {};
        clickDelegate = null;
        changeDelegate = null;
        args = null;
        option = null;

        for (prop in this.__columnDefs) {
            if (this.__columnDefs.hasOwnProperty(prop)) {
                cell = document.createElement("td");
                if (editColumns[prop] && editMode) {
                    editColumn = editColumns[prop];
                    editType = Epic.Training.Core.Controls.Grid.Web.CollectionGrid.EditTypes;
                    switch (editColumn.Type) {
                        case editType.Checkbox:
                            checkboxBugFixes = checkboxBugFixes || [];
                            cellContent = document.createElement("input");
                            cellContent.type = "checkbox";
                            cellContent.id = this.__id + "_" + pos + "_" + prop;

                            // This updates the checkbox in Chrome/IE8, but for some reason doesn't work in IE9
                            cellContent.checked = !!rowObject[prop];

                            // To handle the IE9 bug, set the checbox to its value again later if it should be checked:
                            if (cellContent.checked === true) {
                                checkboxBugFixes.push(Function.createCallback(Function.createDelegate(this, this.__updateCheckbox), { id: cellContent.id, value: !!rowObject[prop] }));
                            }
                            args = { property: prop, object: rowObject, checkbox: cellContent };
                            $addHandler(cell, "click", Function.createCallback(this.__checkboxClickDelegate, args));
                            break;
                        case editType.TextArea:
                            cellContent = document.createElement("textarea");
                            cellContent.rows = editColumn.Options.Rows;
                            cellContent.cols = editColumn.Options.Cols;
                            cellContent.value = rowObject[prop];
                            args = { property: prop, object: rowObject };
                            $addHandler(cellContent, "blur", Function.createCallback(this.__controlBlurDelegate, args));
                            break;
                        case editType.Text:
                            cellContent = document.createElement("input");
                            cellContent.type = "text";
                            cellContent.value = rowObject[prop];
                            args = { property: prop, object: rowObject };
                            $addHandler(cellContent, "blur", Function.createCallback(this.__controlBlurDelegate, args));
                            break;
                        case editType.Select:
                            cellContent = document.createElement("select");
                            for (value in editColumn.Options) {
                                if (editColumn.Options.hasOwnProperty(value)) {
                                    option = document.createElement("option");
                                    cellContent.options.add(option);
                                    option.value = value;
                                    option.text = editColumn.Options[value];
                                }
                            }
                            // Set the value of the control
                            cellContent.value = rowObject[prop];
                            args = { property: prop, object: rowObject };
                            $addHandler(cellContent, "blur", Function.createCallback(this.__controlBlurDelegate, args));
                            break;
                    }
                }
                else if (imageColumns[prop]) {
                    cellContent = document.createElement("img");
                    cellContent.setAttribute("src", rowObject[prop]);
                    cellContent.setAttribute("alt", imageColumns[prop]);
                }
                else {

                    cellContent = document.createTextNode(rowObject[prop]);
                }
                cell.appendChild(cellContent);
                Sys.UI.DomElement.addCssClass(cell, this.__columnDefs[prop].CssClass);
                row.appendChild(cell);
            }
        }

        //Attach row option handlers
        if (this.__rowOptionHandlers) {
            cell = document.createElement("td");
            Sys.UI.DomElement.addCssClass(cell, this.__cssClassList.rowOptions);
            for (option in this.__rowOptionHandlers) {
                if (this.__rowOptionHandlers.hasOwnProperty(option)) {
                    // Only display row options that match the current mode
                    if ((this.__rowOptionDisplayModes[option] === Epic.Training.Core.Controls.Grid.Web.CollectionGrid.RowOptionDisplayModes.IsReadOnly && editMode) ||
                    (this.__rowOptionDisplayModes[option] === Epic.Training.Core.Controls.Grid.Web.CollectionGrid.RowOptionDisplayModes.NotReadOnly && !editMode)) {
                        continue;
                    }


                    a = document.createElement("a");

                    handlers = this.__rowOptionHandlers[option];

                    for (idx = 0; idx < handlers.length; idx++) {
                        callback = Function.createCallback(handlers[idx], rowObject);
                        $addHandler(a, 'click', callback);
                    }

                    javascript = "javascript";
                    a.setAttribute("href", javascript + ":;");
                    if (this.__rowOptionTooltips[option]) {
                        a.setAttribute("title", this.__rowOptionTooltips[option]);
                    }

                    a.appendChild(document.createTextNode(option));
                    cell.appendChild(a);
                    cell.appendChild(document.createTextNode(" "));
                }
            }


            row.appendChild(cell);
        }

        // Pass checkbox bug fixes out so that they can be applied after
        // the row is added to the DOM
        this.__ceckboxBugFixes = checkboxBugFixes;

        return row;
    },

    __controlLostFocus: function Core$CollectionGrid$__controlLostFocus(event, args) {
        /// <summary>(private) Called when a form element embedded in the Grid loses focus.  Updates the bound object with
        ///  the new value in the control.</summary>
        /// <param name="event" type="Object">The event object</param>
        /// <param name="args" type="Object">Additional arguments</param>

        var rowObject, prop, newValue;

        rowObject = args.object;
        prop = args.property;
        newValue = event.target.value;
        rowObject[prop] = newValue;
    },

    __checkboxCellClicked: function Core$CollectionGrid$__checkboxCellClicked(src, args) {

        var rowObject, prop, checkbox, newValue;

        rowObject = args.object;
        prop = args.property;
        checkbox = args.checkbox;
        newValue = !rowObject[prop];
        checkbox.checked = newValue;
        rowObject[prop] = newValue;
    },

    __selectRow: function Core$CollectionGrid$__selectRow(event, args) {
        ///<summary>(private) Selects the object corresponding to the clicked row.</summary>
        ///<param name="event" type="Object">The click event object</param>
        ///<param name="args" type="Array">Array of event arguments where position 0 
        ///  contains the radio button element of the row, and position 2 contains
        ///  the row object.</param>
        var radio = args[0];
        radio.checked = true;
        this.__selectedObject = args[1];
    }
    //#endregion
};



//#region static methods
Epic.Training.Core.Controls.Grid.Web.CollectionGrid.createRadio = function Core$CollectionGrid$createRadio(name, num) {
    ///<summary>Create a readio button.  Implemented so that it works in IE, Firefox and Chrome</summary>
    ///<param name="name" type="String">The name of the radio button group</param>
    ///<param name="control" type="Object">The position of this radio button in the group.  Used to assign a unique id</param>
    ///<returns type="Object">Returns an object representing the new radio button</returns>
    var radio = null;
    try {  // Works in IE, but throws errors in other browsers
        radio = document.createElement("<input name='" + name + "' type='radio' id='" + name + "$" + num + "' />");
    }
    catch (e) {  // Works in Chrome & Firefox
        radio = document.createElement("input");
        radio.setAttribute("type", "radio");
        radio.setAttribute("name", name);
        radio.setAttribute("id", name + "$" + num);
    }
    return radio;
};
//#endregion

//#region string lists and enumerated types
Epic.Training.Core.Controls.Grid.Web.CollectionGrid.EditTypes = function Core$CollectionGrid$EditTypes() {
    /// <summary>Edit types of columns in a CollectionGrid</summary>
    /// <field name="Checkbox" type="Number" integer="true" static="true">Column displays as a checkbox in edit mode</field>
    /// <field name="Select" type="Number" integer="true" static="true">Column displays as a select control in edit mode</field>
    /// <field name="Text" type="Number" integer="true" static="true">Column displays as a textbox in edit mode</field>
    /// <field name="TextArea" type="Number" integer="true" static="true">Column displays as a textarea control in edit mode</field>
    throw Error.notImplemented("Cannot create an instance of a enumerated type");
};
Epic.Training.Core.Controls.Grid.Web.CollectionGrid.EditTypes.prototype = {
    Checkbox: 0,
    Select: 1,
    Text: 2,
    TextArea: 3
};
Epic.Training.Core.Controls.Grid.Web.CollectionGrid.EditTypes.registerEnum("Epic.Training.Core.Controls.Grid.Web.CollectionGrid.EditTypes");

Epic.Training.Core.Controls.Grid.Web.CollectionGrid.RowOptionDisplayModes = function Core$CollectionGrid$RowOptionDisplayModes() {
    /// <summary>Display modes of row options</summary>
    /// <field name="Both" type="Number" integer="true" static="true">The row option is displayed for both edit and read-only modes</field>
    /// <field name="IsReadOnly" type="Number" integer="true" static="true">The row option is displayed in read-only mode</field>
    /// <field name="NotReadOnly" type="Number" integer="true" static="true">The row option is displayed in edit mode</field>
    throw Error.notImplemented("Cannot create an instance of a enumerated type");
};
Epic.Training.Core.Controls.Grid.Web.CollectionGrid.RowOptionDisplayModes.prototype =
{
    Both: 0,
    IsReadOnly: 1,
    NotReadOnly: 2
};
Epic.Training.Core.Controls.Grid.Web.CollectionGrid.RowOptionDisplayModes.registerEnum("Epic.Training.Core.Controls.Grid.Web.CollectionGrid.RowOptionDisplayModes");
//#endregion

// Register the class (MS AJAX lib)
Epic.Training.Core.Controls.Grid.Web.CollectionGrid.registerClass("Epic.Training.Core.Controls.Grid.Web.CollectionGrid", Sys.UI.Control);

