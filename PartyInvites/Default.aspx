<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PartyInvites.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Year Eve's</title>
	<link rel="stylesheet" href="PartyStyles.css" />
</head>
<body>
    <form id="rsvpform" runat="server">
        <div>
            <h2>New Year's Eve @ PJ's!</h2>
            <p>Welcome.</p>
        </div>
        <div><label>Your name:</label><input runat="server" type="text" id="name" /></div>
        <div><label>Your email:</label><input runat="server" type="text" id="email" /></div>
        <div><label>Your phone:</label><input runat="server" type="text" id="phone" /></div>
        <div>
            <label>Will you be coming?</label>
            <select runat="server" id="willattend">
                <option value="">Choose an option</option>
                <option value="true">Yes</option>
                <option value="false">No</option>
            </select>
        </div>
        <div>
            <button type="submit">Submit RSVP</button>
        </div>
    </form>
</body>
</html>
