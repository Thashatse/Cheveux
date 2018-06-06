<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheveuxHelpCenter.aspx.cs" Inherits="Cheveux.Help.CheveuxHelpCenter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cheveux Help Center</title>
    <!-- Bootstrap-->
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <!--CSS-->
    <link rel="“stylesheet”" type="“text/css”" href="“/CSS/Cheveux.css”">
</head>
<body>
    <form id="HelpForm" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <!--Jumbotron Header-->
                        <div class="jumbotron">
                            <img src="/IMG_0715.png" alt="logo" width="150" height="150" />
                            <h1>Help Center</h1>
                            <p>Welcome to the Cheveux Help Center</p>
                        </div>

                        <!--Nav Pills-->
                        <ul class="nav nav-pills nav-stacked">
                            <li class="active"><a href="../Default.aspx">Cheveux Home </a></li>
                            <li><a href="#1">Help 1 </a></li>
                            <li><a href="#2">Help 2 </a></li>
                            <li><a href="#3">Help 3 </a></li>
                        </ul>

                    </div>
                </div>

                <!--Help Section Template -->
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <a name="1"></a>
                        <!--Header-->
                        <h2>Section 1 </h2>
                        <!--Content-->
                        <p>Content</p>
                    </div>
                </div>

                <!--Help Section Template -->
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <a name="2"></a>
                        <!--Header-->
                        <h2>Section 2 </h2>
                        <!--Content-->
                        <p>Content</p>
                    </div>
                </div>

                <!--Help Section Template 3-->
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <a name="3"></a>
                        <!--Header-->
                        <h2>Section 3 </h2>
                        <!--Content-->
                        <p>Content</p>
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
