<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BillingInfo.ascx.cs" Inherits="Epic.Training.Example.Web.Pages.Inventory.BillingInfo" %>
<asp:Label ID="lblType" runat="server" Text="Type of card: "
           AssociatedControlID="lstType"/><br />

<asp:ListBox ID="lstType" runat="server">
    <asp:ListItem Text="Visa" Value="visa"/>
    <asp:ListItem Text="Master Card" Value="ms"/>
    <asp:ListItem Text="American Express" Value="amx"/>
</asp:ListBox><br />

<asp:Label ID="lblNumber" runat="server" Text="Card #: " 
           AssociatedControlID="txtNumber" /><br />
<asp:TextBox ID="txtNumber" runat="server" Text="" /><br />

<asp:Label ID="lblDate" runat="server" Text="Expires on: " 
           AssociatedControlID="txtDate" /><br />
<asp:TextBox ID="txtDate" runat="server" Text="" /><br />
