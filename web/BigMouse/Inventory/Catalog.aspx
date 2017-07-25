<%@ Page Title="" Language="C#" MasterPageFile="~/ExampleMaster.Master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="Epic.Training.Example.Web.Pages.Inventory.Catalog" %>
<%@ Register Assembly="Epic.Training.Core.Controls.Grid.Web" 
	Namespace="Epic.Training.Core.Controls.Grid.Web"
	TagPrefix="etcw" %>
<%@ Register Assembly="Epic.Training.Core.Controls.Web"
	Namespace="Epic.Training.Core.Controls.Web"
	TagPrefix="etcw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
	<h1>Catalog</h1>
	<%--asp:Table ID="tblInventory" runat="server" CssClass="tblData">
		<asp:TableHeaderRow>
			<asp:TableHeaderCell>Name</asp:TableHeaderCell>
			<asp:TableHeaderCell>Image</asp:TableHeaderCell>
			<asp:TableHeaderCell>Price</asp:TableHeaderCell>
			<asp:TableHeaderCell>Details</asp:TableHeaderCell>
		</asp:TableHeaderRow>
		<asp:TableRow>
			<asp:TableCell>Blue</asp:TableCell>
			<asp:TableCell>
				<asp:Image ImageUrl="~/Inventory/images/Blue-Thumb.png" runat="server" />
			</asp:TableCell>
			<asp:TableCell>90.00</asp:TableCell>
			<asp:TableCell>
				<asp:HyperLink NavigateUrl="~/Inventory/Details.aspx" runat="server">View</asp:HyperLink>
			</asp:TableCell>
		</asp:TableRow>
	</asp:Table--%>
	<button id="elServiceTest" type="button" class="hidden">Run service test</button>
	<etcw:CollectionGrid ID="gridInventory" runat="server"
		ShowHeaders="true" CssClass="tblData" ReadOnly="true">
		<ColumnDefinitions>
			<etcw:EditColumn Header="Name" BoundProperty="Name" Type="Text" />
			<etcw:EditColumn Header="Image" BoundProperty="ImagePathSmall" Type="Select"
				DisplayType="Image" AlternateText="Thumbnail of cheese">
				<SelectOptions>
					<etcw:SelectOption DisplayText="American" Value="./images/American-Thumb.png" />
					<etcw:SelectOption DisplayText="Blue" Value="./images/Blue-Thumb.png" />
					<etcw:SelectOption DisplayText="Cheddar" Value="./images/Cheddar-Thumb.png" />
					<etcw:SelectOption DisplayText="Gouda" Value="./images/Gouda-Thumb.png" />
					<etcw:SelectOption DisplayText="Mozzarella" Value="./images/Mozzarella-Thumb.png" />
					<etcw:SelectOption DisplayText="Swiss" Value="./images/Swiss-Thumb.png" />
					<etcw:SelectOption DisplayText="Other" Value="./images/GenericCheese-Thumb.png" />
				</SelectOptions>
			</etcw:EditColumn>
			<etcw:EditColumn Header="Description" BoundProperty="Description" Type="TextArea" Rows="4" Cols="20" />
		</ColumnDefinitions>
		<RowOptions>
			<etcw:Option Name="Details" ToolTip="View or edit details about this cheese" RowOptionDisplayMode="IsReadOnly" />
			<etcw:Option Name="Edit" ToolTip="Edit this cheese in the table" RowOptionDisplayMode="IsReadOnly" />
			<etcw:Option Name="Done" ToolTip="Done editing this cheese" RowOptionDisplayMode="NotReadOnly" />
		</RowOptions>
		<TableOptions>
			<etcw:Option Name="Add cheese" ToolTip="Add a new cheese to the inventory" />
		</TableOptions>
	</etcw:CollectionGrid>
	<asp:ScriptManagerProxy runat="server">
		<Services>
			<asp:ServiceReference Path="~/Inventory/CatalogBehavior.svc" />
		</Services>
	</asp:ScriptManagerProxy>
	<etcw:PageSettings runat="server" ClientScriptPath="~/Inventory/CatalogBehavior.js"
		ClientClass="Epic.Training.Example.Web.Pages.Inventory.CatalogBehavior">
		<ManagedControls>
			<etcw:ControlEntry ControlID="elServiceTest" />
			<%--etcw:ControlEntry ControlID="gridInventory" /--%>
		</ManagedControls>
	</etcw:PageSettings>
</asp:Content>
