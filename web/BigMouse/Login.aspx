<%@ Page Title="" Language="C#" MasterPageFile="~/ExampleMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Epic.Training.Example.Web.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
	<asp:Login ID="Login1" runat="server" DisplayRememberMe="false" CssClass="loginTable">
		<TitleTextStyle CssClass="loginTitle" />
	</asp:Login>
</asp:Content>
