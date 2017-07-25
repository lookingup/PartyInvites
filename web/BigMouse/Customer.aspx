<%@ Page Title="" Language="C#" MasterPageFile="~/ExampleMaster.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="Epic.Training.Example.Web.Pages.Customer" %>
<%@ Register Namespace="Epic.Training.Core.Controls.Web" Assembly="Epic.Training.Core.Controls.Web" TagPrefix="etcw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
	<h1 id="hMain">Customer Service</h1>
	<h2>Welcome</h2>
	<p>Welcome to the Big Mouse Cheese Factory's customer service page! As our valued customer, your concerns are very important to us.</p> 
	<p>The Big Mouse Cheese Factory is a multinational company and we strive to provide the best service for all of our customers. If you would like to speak with a customer representative, use the contact information below corresponding to your country of business, or send us an e-mail.</p>
	<h3>Phone/Mail</h3>
	
	<table class="tblData">
		<tr>
			<th>Country</th><th>Phone</th><th>Address</th>
		</tr>
		<tr>
			<td>US</td>
			<td>608-271-9000</td>
			<td>1979 Milky Way <br />Verona, WI 53593</td>
		</tr>
		<tr>
			<td>UK</td>
			<td>44-203-002-4300</td>
			<td>London, United Kingdom, SE1 2BY <br />5 More London Place</td>
		</tr>
		<tr>
			<td>France</td>
			<td>33-1-4448-5600</td>
			<td>18 Avenue de Suffren <br />Paris, France, 75015</td>
		</tr>
	</table>
	
	<h3>E-mail</h3>
	<img runat="server" style="float:right;" alt="Email mouse" src="~/images/BigMouse-Contact-256x257.png"
		width="200" height="200" />

	<table class="tblLayout">
		<tr>
			<td>
				<label for="selCountry">Country:</label>
			</td>
			<td>
				<select id="selCountry">
					<option value="us">US</option>
					<option value="uk">UK</option>
					<option value="fr">France</option>
				</select>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label AssociatedControlID="txtName" runat="server">Name:</asp:Label>
			</td>
			<td>
				<input type="text" id="txtName" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label AssociatedControlID="txtEmail" runat="server">Email:</asp:Label>
			</td>
			<td>
				<input type="text" id="txtEmail" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				<label for="txtSubject">Subject:</label>
			</td>
			<td>
				<input type="text" id="txtSubject" />
			</td>
		</tr>
		<tr>
			<td>
				<label for="areaMessage">Message:</label>
			</td>
			<td>
				<textarea id="areaMessage" rows="5" cols="17"></textarea>
			</td>
		</tr>
		<tr>
			<td></td>
			<td>
				<input id="chkUrgent" type="checkbox" />
				<label for="chkUrgent">Urgent?</label><br />
			</td>
		</tr>
		<tr>
			<%-- <button id="btnSend" type="submit" title="Send message">Send</button> --%>
			<td></td>
			<td>
				<asp:Button runat="server" ID="btnSend" Text="Send" ToolTip="Send message" OnClientClick="return false;" />
				<button id="btnClear" type="reset" title="Clear form">Clear</button>
			</td>
		</tr>
	</table>

	<div id="divError" class="error hidden"></div>

	<%--asp:ScriptManagerProxy ID="smp" runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/CustomerBehavior.js" />
		</Scripts>
	</asp:ScriptManagerProxy --%>
	
	<!-- Page Settings -->
	<etcw:PageSettings runat="server"
		ClientClass="Epic.Training.Example.Web.Pages.CustomerBehavior" 
		ClientScriptPath="~/CustomerBehavior.js">
		<ManagedControls>
			<etcw:ControlEntry ControlID="btnClear" />
			<etcw:ControlEntry ControlID="btnSend" />
			<etcw:ControlEntry ControlID="txtName" />
			<etcw:ControlEntry ControlID="txtEmail" />
			<etcw:ControlEntry ControlID="selCountry" />
			<etcw:ControlEntry ControlID="txtSubject" />
			<etcw:ControlEntry ControlID="areaMessage" />
			<etcw:ControlEntry ControlID="chkUrgent" />
			<etcw:ControlEntry ControlID="divError" />
			<%--etcw:ControlEntry ControlID="clkMain" /--%>
		</ManagedControls>
	</etcw:PageSettings>
</asp:Content>
