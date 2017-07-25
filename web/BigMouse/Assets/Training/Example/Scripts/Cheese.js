/// <reference name="MicrosoftAjax.js" />

//***************************
//  Copyright 2016 Epic Systems Corporation
//***************************

Type.registerNamespace("Epic.Training.Example.Web");

Epic.Training.Example.Web.Cheese = function Example$Cheese(referenceObject)
{
	/// <summary>Class representing a cheese.</summary>
	/// <field name="Name" type="String">(public)Name of the cheese</field>
	/// <field name="ID" type="Number">(public)ID of the cheese</field>
	/// <field name="ImagePathSmall" type="String">(public)Thumbnail image path</field>
	/// <field name="ImagePathLarge" type="String">(public)Full image path</field>
	/// <field name="Weight" type="Number">(public)Weight in pounds</field>
	/// <field name="Description" type="String">(public)Description of the cheese</field>
	/// <field name="Price" type="Number">(public)Cost in dollars</field>

	var key;

	Epic.Training.Example.Web.Cheese.initializeBase(this);

	// If the reference object has been passed, copy
	// properties over that are defined in the prototype of this class
	if (referenceObject) // Check for null/undefined
	{
		for (key in referenceObject) // Iterate over properties
		{
			if (referenceObject.hasOwnProperty(key) && typeof (this[key]) !== "undefined")
			{
				this[key] = referenceObject[key]; // Copy the value over
			}
		}
	}
};

Epic.Training.Example.Web.Cheese.prototype = {
	//#region fields
	Name: "New Cheese",
	Item: -1,
	ImagePathSmall: "./images/GenericCheese-Thumb.png",
	ImagePathLarge: "",
	Weight: 0,
	Description: "Fresh!",
	Price: 0,
	//#endregion

	//#region public methods 
	toString: function Example$Cheese$toString() 
	{ 
		///<summary>Get the string representaiton of this cheese</summary> 
		///<returns type="String" /> 
		var result = this.Item.toString() + " - ";
		if (this.Name.length === 0)
		{
			result += "<no name>";
		}
		else
		{
			result += this.Name;
		}
		return result;
	}, //#endregion
};

Epic.Training.Example.Web.Cheese.registerClass("Epic.Training.Example.Web.Cheese");
