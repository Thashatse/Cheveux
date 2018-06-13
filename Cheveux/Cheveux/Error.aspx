<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Cheveux.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error - Cheveux</title>
    <!-- Bootstrap-->
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <!--CSS-->
    <link rel="“stylesheet”" type="“text/css”" href="“/CSS/Cheveux.css”">
</head>
<body>
    <form id="ErrorForm" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="jumbotron">
                            <h1>An Error Occurred</h1>
                            <!--Error details placehoders-->
                            <asp:Label ID="ErrorHeader" runat="server"></asp:Label><br />
                            <asp:Label ID="Error1" runat="server"></asp:Label>
                            <!--Line Break-->
                            <br />
                            <br />
                            <!--Help-->
                            <a href="Help/CheveuxHelpCenter.aspx" target="_blank">Help Center</a>
                            <!--Line Break-->
                            <br />
                            <br />
                            <a href="Default.aspx">Return Home</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!--Bootstrap-->
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>