/// <reference name="MicrosoftAjax.js" />
/// <reference path="~/Shared.js" />
/// <reference path="~/Assets/Training/Core/Scripts/CollectionGrid.js" />
/// <reference path="~/Assets/Training/Core/Scripts/PageBehavior.js" />
/// <reference path="~/Assets/Training/Example/Scripts/Cheese.js" />
/// <reference path="CatalogBehavior.svc" />
//***************************
//  Copyright 2016 Epic Systems Corporation
//***************************

/*global alert, $addHandler, document */

Type.registerNamespace("Epic.Training.Example.Web.Pages.Inventory");

Epic.Training.Example.Web.Pages.Inventory.CatalogBehavior = function Example$CatalogBehavior(clientId)
{
	/// <summary>PageBehavior for cheese catalog (inventory) page</summary>
	/// <field name="__shared" type="Epic.Training.Example.Web.Pages.Shared">(private)Shortcut to the shared library</field>
	/// <field name="__gridInventory" type="Epic.Training.Core.Controls.Grid.Web.CollectionGrid">(private)Grid to display inventory</field>
	/// <field name="__curEditCheese" type="Epic.Training.Example.Web.Cheese">(private)The cheese currently being edited</field>
	/// <field name="__service" type="Epic.Training.Example.Web.Pages.Inventory.Services.CatalogBehavior">(private)The web service corresponding to this Javascript class</field>
	/// <field name="__saveDelegate" type="Function">(private) Called when the cheese is saved</field> 
	/// <field name="__saveErrorDelegate" type="Function">(private) Called when the cheese cannot be saved</field>
	Epic.Training.Example.Web.Pages.Inventory.CatalogBehavior.initializeBase(this, [clientId]);
};

Epic.Training.Example.Web.Pages.Inventory.CatalogBehavior.prototype = {

	//#region fields
	__shared: Epic.Training.Example.Web.Pages.Shared,
	__gridInventory: null,
	__curEditCheese: null,
	__service: null,
	__saveDelegate: null,
	__saveErrorDelegate: null,
	//#endregion

	//#region overridden methods from PageBehavior
	onDataPreload: function Example$CatalogBehavior$onDataPreload()
	{
		/// <summary>Called by the constructor before "onLoad".</summary>
		this.__service = Epic.Training.Example.Web.Pages.Inventory.Services.CatalogBehavior;
	},

	onLoad: function Example$CatalogBehavior$onLoad()
	{
		/// <summary>Called by the constructor after "onDataPreload" and before "onLoaded"</summary>
		var loadHandler = $$fcd(this, this.onDataLoaded);
		//this.__shared.GetCheeses(loadHandler);
		this.__service.GetCheeses(loadHandler);
	},

	onLoaded: function Example$CatalogBehavior$onLoaded()
	{
		/// <summary>Called by the constructor after "onLoad".</summary>
		this.__gridInventory = this.getCtl("gridInventory");

		var detailsDelegate = $$fcd(this, this.__gridInventoryDetailsClick);
		var editDelegate = $$fcd(this, this.__gridInventoryEditClick);
		var doneDelegate = $$fcd(this, this.__gridInventoryDoneClick);
		var addDelegate = $$fcd(this, this.__gridInventoryAddClick);

		this.__gridInventory.addRowOptionHandler("Details", detailsDelegate);
		this.__gridInventory.addRowOptionHandler("Edit", editDelegate);
		this.__gridInventory.addRowOptionHandler("Done", doneDelegate);
		this.__gridInventory.addTableOptionHandler("Add cheese", addDelegate);

		$addHandler(this.getEl("elServiceTest"), "click", $$fcd(this, this.__serviceTest));

		this.__saveDelegate = $$fcd(this, this.__cheeseSaved);
		this.__saveErrorDelegate = $$fcd(this, this.__saveError);
	},
	//#endregion

	//#region event handlers
	onDataLoaded: function Example$CatalogBehavior$onDataLoaded(cheeses)
	{
		/// <summary>Event handler called when data is ready to be displayed</summary>
		/// <param name="cheeses" type="Array">An array of Cheese objects</param>
		var ln, newCheese;
		for (ln = 0; ln < cheeses.length; ln++)
		{
			newCheese = new Epic.Training.Example.Web.Cheese(cheeses[ln]);
			this.__gridInventory.addRow(newCheese);
		}
	},

	__gridInventoryDetailsClick: function Example$CatalogBehavior$__gridInventoryDetailsClick(event, cheese) 
	{
		/// <summary>(private) Click event handler of the gridInventory row option Details</summary>
		/// <param name="event" type="Object">The click event argument</param>
		/// <param name="cheese" type="Epic.Training.Example.Web.Cheese">Cheese object of the clicked row</param>
		if (this.__validateAndSaveCheese())
		{
			document.location.href = "./Details.aspx?item=" + cheese.Item;
		}
	},

	__gridInventoryEditClick: function Example$CatalogBehavior$__gridInventoryEditClick(event, cheese) 
	{
		this.__editCheese(cheese);
		//alert("Edit clicked");
	},

	__gridInventoryDoneClick: function Example$CatalogBehavior$__gridInventoryDoneClick(event, cheese)
	{
		/// <summary>(private) Handle the click event of the Done row option</summary>
		/// <param name="event" type="Object">The click event argument</param>
		/// <param name="cheese" type="Epic.Training.Example.Web.Cheese">Cheese object of the clicked row</param>

		if (this.__curEditCheese === cheese)
		{
			this.__validateAndSaveCheese();
			this.__gridInventory.select(cheese);
		}
		else
		{
			throw new Error("No cheese is currently being edited. Done should be hidden.");
		}
		//alert("Done clicked");
	},

	__gridInventoryAddClick: function Example$CatalogBehavior$__gridInventoryAddClick(event)
	{
		/// <summary>Handle the click event of the Add cheese table option in gridInventory</summary>
		/// <param name="event" type="Object">The click event object</param>

		if (this.__validateAndSaveCheese())
		{
			var newCheese = new Epic.Training.Example.Web.Cheese();
			this.__gridInventory.addRow(newCheese);
			this.__editCheese(newCheese);
		}
		//alert("Add clicked");
	},

	__serviceTest: function Example$CatalogBehavior$__serviceTest()
	{
		/// <summary>Run the web service overlap test</summary>
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
		this.__service.MyServiceMethod();
	},
	//#endregion

	//#region private methods
	__validateAndSaveCheese: function Example$CatalogBehavior$__validateAndSaveCheese()
	{
		/// <summary>Save cheese to database, then release edit mode.</summary>
		/// <returns type="Boolean">true if successful, false otherwise</returns>

		if (this.__curEditCheese !== null)
		{
			//TODO: call web service
			this.__service.AddOrReplaceCheese(this.__curEditCheese, this.__saveDelegate,
				this.__saveErrorDelegate, this.__curEditCheese);
			this.__gridInventory.set_rowReadOnly(this.__curEditCheese, true);
			this.__curEditCheese = null;
		}
		return true;
	},

	__editCheese: function Example$CatalogBehavior$__editCheese(cheese)
	{
		/// <summary>(private) Set the cheese being edited to a different cheese</summary>
		/// <param name="cheese" type="Epic.Training.Example.Web.Cheese">The new cheese to edit</param>
		
		if (this.__curEditCheese !== cheese)
		{
			if (this.__validateAndSaveCheese()) //save previous cheese before editing the new one
			{
				this.__gridInventory.set_rowReadOnly(cheese, false);
				this.__curEditCheese = cheese;
				this.__gridInventory.select(cheese); //set focus
			}
		}
	},

	__cheeseSaved: function Example$CatalogBehavior$__cheeseSaved(item, cheese) 
	{ 
		/// <summary>Called when the cheese save is a success. Sets the Item 
		/// number of the cheese to the new server-assigned number.</summary> 
		/// <param name="item" type="Number">Item number assigned by the server (ie, record ID).</param> 
		/// <param name="cheese" type="Epic.Training.Example.Web.Cheese">The cheese that was just saved</param> 
		cheese.Item = item;
	},

	__saveError: function Example$CatalogBehavior$__saveError(error, cheese) 
	{ 
		/// <summary>Display a message if there was a problem saving the cheese</summary> 
		/// <param name="error" type="Object">The error returned from the server</param> 
		/// <param name="cheese" type="Epic.Training.Example.Web.Cheese"> 
		/// The cheese that could not be saved</param> 
		alert("There was a problem saving the following cheese: \n\n\t" + cheese.toString() + "\n\n" + error.get_message());
		this.__editCheese(cheese);
	},
	//#endregion
};

Epic.Training.Example.Web.Pages.Inventory.CatalogBehavior.registerClass("Epic.Training.Example.Web.Pages.Inventory.CatalogBehavior", Epic.Training.Core.Controls.Web.PageBehavior);
