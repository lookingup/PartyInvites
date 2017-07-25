/// <reference path="Shared.js" />
/// <reference path="~/Assets/Training/Core/Scripts/PageBehavior.js" />
//***************************
//  Copyright 2016 Epic Systems Corporation
//***************************

/*global alert, $addHandler */

Type.registerNamespace("Epic.Training.Example.Web.Pages");

Epic.Training.Example.Web.Pages.CustomerBehavior = function Example$CustomerBehavior(clientId)
{
	///<field name="__shared" type="Epic.Training.Example.Web.Pages.Shared">Shortcut to the shared library</field>
	///<field name="__txtName" type="HTMLInputElement">Name field from the email form</field>
	//TODO: doc comments for remaining fields
	Epic.Training.Example.Web.Pages.CustomerBehavior.initializeBase(this, [clientId]);
};

Epic.Training.Example.Web.Pages.CustomerBehavior.prototype = {

	//#region fields
	__shared: Epic.Training.Example.Web.Pages.Shared,
	__txtName: null,
	__txtEmail: null,
	__selCountry: null,
	__txtSubject: null,
	__areaMessage: null,
	//#endregion

	//#region event handlers
	__btnSendClick: function Example$CustomerBehavior$__btnSendClick(event) 
	{
		///<summary>Click event handler for btnSend</summary>
		//alert("Send clicked: " + event.target.id);
		
		var name = this.__txtName.value.trim();
		var email = this.__txtEmail.value.trim();
		var country = this.__selCountry.value.trim();
		var subject = this.__txtSubject.value.trim();
		var message = this.__areaMessage.value.trim();
		
		//var resp = "";
		var details = [];
		if (name === null || name === "")
		{
			details.push({ message: "Name is required.", control: this.__txtName });
		}
		if (email === null || email === "")
		{
			details.push({ message: "Email is required.", control: this.__txtEmail });
		}
		if (country === null || country === "")
		{
			details.push({ message: "Country is required.", control: this.__selCountry });
		}
		if (subject === null || subject === "")
		{
			details.push({ message: "Subject is required.", control: this.__txtSubject });
		}
		if (message === null || message === "")
		{
			details.push({ message: "Message is required.", control: this.__areaMessage });
		}
		
		//if (resp !== "") { resp = "Please complete the following fields before sending:" + resp; }
		if (email && !this.__shared.isEmail(email))
		{
			details.push({ message: "Please enter a valid email address.", control: this.__txtEmail });
		}
			//if (resp !== "") { alert(resp); }
		this.__shared.displayFormErrors("Please fix the following errors before submitting:", this.getEl("divError"), details);

		if (details.length === 0)
		{
			alert("Email sent!");
			$$asInputEl(this.getEl("btnClear")).form.reset();
		}
	},

	__btnClearClick: function Example$CustomerBehavior$__btnClearClick() 
	{
		$$dom.addCssClass(this.getEl("divError"), "hidden");
	},
	//#endregion
	
	toString: function Example$CustomerBehavior$toString() {
		///<summary>A string representation of CustomerBehavior</summary>
		/// <returns type="String" />
		return "Epic.Training.Example.Web.Pages.CustomerBehavior";
	},

	//#region overridden methods from PageBehavior
	onDataPreload: function Example$CustomerBehavior$onDataPreload()
	{
		/// <summary>Called by the CustomerBehavior before "onLoad".</summary>
	},

	onLoad: function Example$CustomerBehavior$onLoad()
	{
		/// <summary>Called by the CustomerBehavior after "onDataPreload" and before "onLoaded"</summary>
	},

	onLoaded: function Example$CustomerBehavior$onLoaded()
	{
		/// <summary>Called by the CustomerBehavior after "onLoad".</summary>
		$addHandler(this.getEl("btnSend"), "click", $$fcd(this, this.__btnSendClick));
		$addHandler(this.getEl("btnClear"), "click", $$fcd(this, this.__btnClearClick));
		this.__txtName = this.getEl("txtName");
		this.__txtEmail = this.getEl("txtEmail");
		this.__selCountry = this.getEl("selCountry");
		this.__txtSubject = this.getEl("txtSubject");
		this.__areaMessage = this.getEl("areaMessage");
	},
	//#endregion
};

Epic.Training.Example.Web.Pages.CustomerBehavior.registerClass("Epic.Training.Example.Web.Pages.CustomerBehavior", Epic.Training.Core.Controls.Web.PageBehavior);

//Epic.Training.Example.Web.Pages.Shared.alert("CustomerBehavior.js");