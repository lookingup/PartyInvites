/// <reference name="MicrosoftAjax.js" />
/// <reference path="~/Assets/Training/Core/Scripts/PageBehavior.js" />
/// <reference path="Shared.js" />
//***************************
//  Copyright 2016 Epic Systems Corporation
//***************************

/*global alert, $addHandler */

Type.registerNamespace("Epic.Training.Example.Web.Pages");

Epic.Training.Example.Web.Pages.ExampleMasterBehavior = function Example$ExampleMasterBehavior(clientId)
{
	Epic.Training.Example.Web.Pages.ExampleMasterBehavior.initializeBase(this, [clientId]);
};

Epic.Training.Example.Web.Pages.ExampleMasterBehavior.prototype = {

	//#region fields
	__clkMain: null,
	//#endregion fields

	//#region overridden methods from PageBehavior
	onDataPreload: function Epic$$ExampleMasterBehavior$onDataPreload()
	{
		/// <summary>Called by the constructor before "onLoad".</summary>
	},

	onLoad: function Example$ExampleMasterBehavior$onLoad()
	{
		/// <summary>Called by the constructor after "onDataPreload" and before "onLoaded"</summary>
	},

	onLoaded: function Example$ExampleMasterBehavior$onLoaded()
	{
		/// <summary>Called by the constructor after "onLoad".</summary>
		var clockDelegate = $$fcd(this, this.__clkMainClick);
		this.__clkMain = this.getCtl("clkMain");
		$addHandler(this.__clkMain.get_element(), "click", clockDelegate)
	},
	//#endregion

	//#region event handlers
	__clkMainClick: function Example$ExampleMasterBehavior$__clkMainClick()
	{
		//alert("Clock was clicked!");
		var isTwentyFourHour = this.__clkMain.get_isTwentyFourHour();
		this.__clkMain.set_isTwentyFourHour(!isTwentyFourHour);
	}
	//#endregion
};

Epic.Training.Example.Web.Pages.ExampleMasterBehavior.registerClass("Epic.Training.Example.Web.Pages.ExampleMasterBehavior", Epic.Training.Core.Controls.Web.PageBehavior);
