<%@ Page Title="" Language="C#" MasterPageFile="~/ExampleMaster.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Epic.Training.Example.Web.Pages.Inventory.Details" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="cphHead" runat="server">
	<link rel="stylesheet" href="Details.css" type="text/css" media="all" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
	<h1>Details</h1>
	  
	<table id="tblControls">
		<tr>
			<td rowspan="7" class="imageColumn">
				<asp:Image ID="imgFull" runat="server" Width="350"
					AlternateText="Full-sized Cheese" ImageUrl="~/inventory/images/Blue-Full.png" />
			</td>
			<td class="detailsLabel">
				<asp:Label ID="lblItem" runat="server" Text="Item:" AssociatedControlID="txtItem" />
			</td>
			<td>
				<asp:TextBox ID="txtItem" runat="server" Text="110" ReadOnly="true" CssClass="detailsField" />
			</td>
		</tr>
		<tr>
			<td class="detailsLabel">
				<asp:Label ID="lblName" runat="server" Text="Name:" AssociatedControlID="txtName" />
			</td>
			<td>
				<asp:TextBox ID="txtName" runat="server" Text="American Blue" CssClass="detailsField editable" />
			</td>
		</tr>
		<tr>
			<td class="detailsLabel">
				<asp:Label ID="lblWeight" runat="server" Text="Weight:" AssociatedControlID="txtWeight" />
			</td>
			<td>
				<asp:TextBox ID="txtWeight" runat="server" Text="25 lb" CssClass="detailsField editable" />
			</td>
		</tr>
		<tr>
			<td class="detailsLabel">
				<asp:Label ID="lblPrice" runat="server" Text="Price:" AssociatedControlID="txtPrice" />
			</td>
			<td>
				<asp:TextBox ID="txtPrice" runat="server" Text="$90.00" CssClass="detailsField editable" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Label ID="lblDescription" runat="server" Text="Description:" AssociatedControlID="txtDescription" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:TextBox ID="txtDescription" runat="server" Text="Strong blue cheese flavor running throughout"
					Wrap="true" Rows="8" TextMode="MultiLine" CssClass="description" />
			</td>
		</tr>
		<tr>
			<td colspan="2" id="tdButtons">
				<asp:Button ID="btnEdit" runat="server" Text="Edit" OnClientClick="return false;" />
				<asp:Button ID="btnAccept" runat="server" Text="Accept" OnClientClick="return false;" />
				<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="return false;" />
			</td>
		</tr>
	</table>
</asp:Content>
