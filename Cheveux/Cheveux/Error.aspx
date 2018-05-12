<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Cheveux.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error - Cheveux</title>
</head>
<body>
    <form id="ErrorForm" runat="server">
        <div>
            <asp:Label ID="ErrorHeader" runat="server"></asp:Label><br />
            <asp:Label ID="Error1" runat="server"></asp:Label>
            <br /><br />
            <a href="Default.aspx">Return Home</a>
        </div>
    </form>
</body>
</html>
