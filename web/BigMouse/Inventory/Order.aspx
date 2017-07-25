<%@ Page Title="" Language="C#" MasterPageFile="~/ExampleMaster.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="Epic.Training.Example.Web.Pages.Inventory.Order" %>
<%@ Register Src="~/Inventory/BillingInfo.ascx" TagName="BillingInfo" TagPrefix="etewp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
	<h1>Place Order</h1>
	<div>
		<asp:Wizard ID="wzdOrder" runat="server" FinishCompleteButtonText="Place Order"
			FinishDestinationPageUrl="~/Inventory/ThankYou.aspx" DisplaySideBar="False">

			<WizardSteps>
				<asp:WizardStep ID="stpSelect" runat="server" Title="Select a Cheese">
					<h2>Order Information</h2>
					<asp:Table ID="tblSelectCheese" runat="server">
						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblCheese" runat="server" Text="Select a cheese:" AssociatedControlID="ddCheese" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:DropDownList ID="ddCheese" runat="server">
									<asp:ListItem>Blue</asp:ListItem>
								</asp:DropDownList>
							</asp:TableCell>
						</asp:TableRow>
						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblQuantity" runat="server" Text="Select quantity:" AssociatedControlID="ddQuantity" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:DropDownList ID="ddQuantity" runat="server">
									<asp:ListItem>1</asp:ListItem>
									<asp:ListItem>2</asp:ListItem>
									<asp:ListItem>3</asp:ListItem>
									<asp:ListItem>4</asp:ListItem>
									<asp:ListItem>5</asp:ListItem>
								</asp:DropDownList>
							</asp:TableCell>
						</asp:TableRow>
					</asp:Table>
				</asp:WizardStep>

				<asp:WizardStep ID="stpShippingAddress" runat="server" Title="Shipping Address">
					<h2>Shipping Address</h2>
					<asp:Table ID="tblShipping" runat="server">

						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblName" runat="server" Text="Name:" AssociatedControlID="txtName" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:TextBox ID="txtName" runat="server" />
							</asp:TableCell>
						</asp:TableRow>

						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblStreetAddress" runat="server" Text="Street Address:" AssociatedControlID="txtStreetAddress" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:TextBox ID="txtStreetAddress" runat="server" />
							</asp:TableCell>
						</asp:TableRow>

						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblCity" runat="server" Text="City:" AssociatedControlID="txtCity" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:TextBox ID="txtCity" runat="server" />
							</asp:TableCell>
						</asp:TableRow>

						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblState" runat="server" Text="State:" AssociatedControlID="txtState" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:TextBox ID="txtState" runat="server" />
							</asp:TableCell>
						</asp:TableRow>

						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblZip" runat="server" Text="Zip:" AssociatedControlID="txtZip" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:TextBox ID="txtZip" runat="server" />
							</asp:TableCell>
						</asp:TableRow>

						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblPhone" runat="server" Text="Phone:" AssociatedControlID="txtPhone" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:TextBox ID="txtPhone" runat="server" />
							</asp:TableCell>
						</asp:TableRow>

						<asp:TableRow>
							<asp:TableCell>
								<asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
							</asp:TableCell>
							<asp:TableCell>
								<asp:TextBox ID="txtEmail" runat="server" />
							</asp:TableCell>
						</asp:TableRow>
					</asp:Table>

				</asp:WizardStep>

				<asp:WizardStep ID="stpShippingMethod" runat="server" Title="Shipping Method">
					<h2>Shipping Method</h2>
					<asp:HiddenField runat="server" ID="hfWeight" />
					<asp:Label ID="lblShippingMethod" runat="server" Text="Shipping Method" AssociatedControlID="lstShipping" />
					<asp:ListBox ID="lstShipping" runat="server">
						<asp:ListItem Text="Ground" Value="ground" Selected="True" />
						<asp:ListItem Text="Second day" Value="2day" />
						<asp:ListItem Text="Next day" Value="1day" />
					</asp:ListBox>
					<div>
						<asp:Button Text="Calculate" ID="btnCalcShip" runat="server" />
						<asp:Label ID="lblShipCost" Text="Shipping cost is: " runat="server" />
					</div>
				</asp:WizardStep>

				<asp:WizardStep ID="stpBillingInfo" runat="server" Title="Billing Information">
					<h2>Billing Information</h2>
					<etewp:BillingInfo ID="biOrder" runat="server" DefaultCard="amx" />
				</asp:WizardStep>

			</WizardSteps>
		</asp:Wizard>
	</div>
</asp:Content>
