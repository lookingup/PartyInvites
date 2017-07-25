/// <reference name="MicrosoftAjax.js" />
/*global Type, Sys, Function, Epic, document, setInterval, clearInterval, $get, $$fcd */
Type.registerNamespace("Epic.Training.Core.Controls.Time.Web");

Epic.Training.Core.Controls.Time.Web.Clock = function Core$Clock(element, clientId, hoursClientId, minutesClientId, secondsClientId, amPmClientId, isTwentyFourHour) {
    ///<summary>The clock class.  Gives the current time down to the second.</summary>
    ///<param name="clientId" type="String">The client id as asigned by ASP.NET.  Used to retrieve objects of contained elements</param>
    ///<param name="hoursClientId" type="String">Client id of the hours span</param>
    ///<param name="minutesClientId" type="String">Client id of the minutes span</param>
    ///<param name="secondsClientId" type="String">Client id of the seconds span</param>
    ///<param name="amPmClientId" type="String">Client id of the AM/PM span</param>
	///<field name="__del" type="Function">Delegate used when the timeout event occurs</field>
	///<field name="__pid" type="Number">Id of the current period</field>
	///<field name="__isTwentyFourHour" type="Boolean">True for a 24h clock, false for a 12h clock</field>
	///<field name="__spanHours" type="HTMLElement">Span displaying hours</field>
	///<field name="__spanMinutes" type="HTMLElement">Span displaying minutes</field>
	///<field name="__spanSeconds" type="HTMLElement">Span displaying seconds</field>
	///<field name="__spanAmPm" type="HTMLElement">Span displaying AM/PM for a 12h clock</field>

    // Calls constructor of Epic.Training.Controls.ControlHandler,
    // which sets up Elements and Objects.
    Epic.Training.Core.Controls.Time.Web.Clock.initializeBase(this, [element]);

    // Get parts of clock from element
	// note $get is an MS Ajax shortcut to document.getElementById
    this.__spanHours = $get(hoursClientId, element);
    this.__spanMinutes = $get(minutesClientId, element);
    this.__spanSeconds = $get(secondsClientId, element);
    this.__spanAmPm = $get(amPmClientId, element);
    this.__isTwentyFourHour = isTwentyFourHour;

    // Set timeout delegate
    this.__del = Function.createDelegate(this, this.__update);

    // Start the periodic update process
    this.__update();
};

Epic.Training.Core.Controls.Time.Web.Clock.prototype =
{
	//#region fields
	__del: null,
	__pid: null,
	__isTwentyFourHour: false,
	__spanHours: null,
	__spanMinutes: null,
	__spanSeconds: null,
	__spanAmPm: null,
	//#endregion

    //#region event handlers
	__update: function Core$Clock$__update()
	{
		///<summary>Called every second to update the stored and displayed time values.</summary>

		// *** Add code here to set the clock to the current time ***
		// *** Be sure to check this.__isTwentyFourHour to set it correctly ***
		// *** For details on getting the current time, Check: 
		// *** http://wiki/main/Foundations/Training/WTC/Reference/JavaScript/Basics/Date_Class 

		//EXERCISE: Set value of each span to display the current time
		var today = new Date();
		if (this.__isTwentyFourHour === true)
		{
			this.__spanHours.innerHTML = today.format("HH");
			this.__spanMinutes.innerHTML = today.format("mm");
			this.__spanSeconds.innerHTML = today.format("ss");
			this.__spanAmPm.innerHTML = "";
		}
		else
		{
			this.__spanHours.innerHTML = today.format("hh");
			this.__spanMinutes.innerHTML = today.format("mm");
			this.__spanSeconds.innerHTML = today.format("ss");
			this.__spanAmPm.innerHTML = today.format("tt");
		}
		// Setup interval if it isn't setup already
        if (!this.__pid) {
            this.__pid = setInterval(this.__del, 1000);
        }
    },
    //#endregion

	//#region properties
	get_isTwentyFourHour: function Core$Clock$get_isTwentyFourHour()
	{
		return this.__isTwentyFourHour;
	},

	set_isTwentyFourHour: function Core$Clock$set_isTwentyFourHour(value)
	{
		this.__isTwentyFourHour = value;
		this.__pid = null;
		this.__update();
	}
	//#endregion
};

// Register the class (MS AJAX lib)
Epic.Training.Core.Controls.Time.Web.Clock.registerClass("Epic.Training.Core.Controls.Time.Web.Clock", Sys.UI.Control);
