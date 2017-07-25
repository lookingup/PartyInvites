/// <reference name="MicrosoftAjax.js" />
/*global alert,document*/

///#region Foundations shortcuts
var $$fcd = Function.createDelegate;
var $$fcc = Function.createCallback;
var $$dom = {
	setInnerText: function Example$Dom$setInnerText(el, text)
	{
		/// <summary>Sets the text content of a dom element</summary> 
		/// <param name="el" type="HTMLElement">dom element</param> 
		/// <param name="text" type="String">text value</param> 
		if (typeof (document.body.textContent) !== "undefined")
		{
			el.textContent = text;
		}
		else
		{
			el.innerText = text;
		}
	},
	getInnerText: function Example$Dom$getInnerText(el)
	{
		/// <summary>Get the text content of a dom element</summary>
		/// <param name="el" type="HTMLElement">dom element</param>
		if (typeof (document.body.textContent) !== "undefined")
		{
			return el.textContent;
		}
		else
		{
			return el.innerText;
		}
	},
	addCssClass: Sys.UI.DomElement.addCssClass,
	removeCssClass: Sys.UI.DomElement.removeCssClass
};
///#endregion

Type.registerNamespace("Epic.Training.Example.Web.Pages");
Epic.Training.Example.Web.Pages.Shared = {
	//#region Sample data and methods to simulate web-service call
	__sampleData: [
        {
        	Name: "Blue", Item: "110",
        	ImagePathSmall: "./images/Blue-Thumb.png",
        	ImagePathLarge: "./images/Blue-Full.png", Weight: "25",
        	Description: "Strong blue cheese flavor running throughout",
        	Price: "80"
        },
        {
        	Name: "Cheddar", Item: "220",
        	ImagePathSmall: "./images/Cheddar-Thumb.png",
        	ImagePathLarge: "./images/Cheddar-Full.png", Weight: "35",
        	Description: "A very mild cheddar flavor.", Price: "76"
        },
        {
        	Name: "Swiss", Item: "320",
        	ImagePathSmall: "./images/Swiss-Thumb.png",
        	ImagePathLarge: "./images/Swiss-Full.png", Weight: "67",
        	Description: "Mellow flavor with many holes", Price: "57"
        }
	],
	GetCheeses: function Example$Shared$GetCheeses(returnDelegate)
	{
		///<summary> A function that returns sample data in a similar 
		/// format to what a web service would return.</summary>
		///<param name="returnDelegate" type="Function">
		/// Function that will be called when the data is returned.</param>

		// Pass sample data as an argument to the return delegate
		var toCall = $$fcc(returnDelegate, this.__sampleData);

		// Call the return delegate, just like an asynchronous call would
		setTimeout(toCall, 1000);
	},
	//#endregion

	alert: function Example$Shared$alert(message)
	{
		/// <summary> Output a string to the user in a popup.
		/// This is an example of a function in a shared module </summary>
		/// <param name="message" type="String">String to display</param>
		alert("Shared alert: " + message);
	},

	isEmail: function Example$Shared$isEmail(str)
	{
		/// <summary> Verifies that the entered string is an email address </summary>
		/// <param name="str" type="String">string to check</param>
		/// <returns type="Boolean">True if the string is a valid email address</returns>
		var email = /^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/;
		return str.search(email) !== -1;
	},

	select: function Example$Shared$select(src, control) {
		///<summary>An event handler function that selects a control or gives it focus
		/// when the event that this function is listening to is raised</summary>
		///<param name="src" type="Object">Element raising the event</param>
		///<param name="control" type="Object">The control to select/give focus to</param>	
		if (control.select) {
			control.select();
		}
		else if (control.focus) {
			control.focus();
		}
	},

	displayFormErrors: function Example$Shared$displayFormErrors(message, messageDiv, details) {
		///<summary>Display form errors in an unordered list.  Each
		/// error is a link to the control containing the error.</summary>
		///<param name="message" type="String">A summary of the set
		///of errors</param>
		///<param name="messageDiv" type="HTMLElement">The div element that
		///will display the message</param>
		///<param name="details" type="Array">Array of details.  Each
		///detail has a reference to "control", the control containing the error 
		/// and "message", a detailed message about the error</param>

		var newStrong, ln, ul, firstLink, li, a, cal, href;

		//#region Remove all parts of previous message
		//while (messageDiv.childNodes.length > 0) {
		//	messageDiv.removeChild(messageDiv.firstChild);
		//}
		messageDiv.innerHTML = "";
		//#endregion

		//#region Add generic error message
		newStrong = document.createElement("strong");
		newStrong.appendChild(document.createTextNode(message));
		messageDiv.appendChild(newStrong);
		//#endregion

		//#region add details of each error
		ul = document.createElement("ul");
		firstLink = null;
		for (ln = 0; ln < details.length; ln++) {
			li = document.createElement("li");
			a = document.createElement("a");
			firstLink = (firstLink === null) ? a : firstLink;

			// Include control with invalid content as an argument to link-click event
			cal = $$fcc(this.select, details[ln].control);
			$addHandler(a, "click", cal);

			// Ensure details appear as links
			href = "javascript";
			a.setAttribute("href", href + ":;");

			// Add message as link display text
			//a.appendChild(document.createTextNode(details[ln].message));
			$$dom.setInnerText(a, details[ln].message);
			li.appendChild(a);
			ul.appendChild(li);
		}
		messageDiv.appendChild(ul);
		//#endregion

		//#region Display or hide the message div
		// If there were details, put focus on the first error and unhide them
		if (firstLink !== null) {
			$$dom.removeCssClass(messageDiv, "hidden");
			firstLink.focus();
		}
		else // If no details, then hide the message div
		{
			$$dom.addCssClass(messageDiv, "hidden");
		}
		//#endregion
	}
};